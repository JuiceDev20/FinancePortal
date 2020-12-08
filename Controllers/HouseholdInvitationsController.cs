using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancePortal.Data;
using FinancePortal.Models;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FinancePortal.Controllers
{
    public class HouseholdInvitationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;


        public HouseholdInvitationsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;

        }

        // GET: HouseholdInvitations
        public async Task<IActionResult> Index()
        {
            return View(await _context.HouseholdInvitation.ToListAsync());
        }

        // GET: HouseholdInvitations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdInvitation = await _context.HouseholdInvitation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdInvitation == null)
            {
                return NotFound();
            }

            return View(householdInvitation);
        }

        // GET: HouseholdInvitations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HouseholdInvitations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseholdId,Expires,RecipientName,Subject,Body,RoleName")] HouseholdInvitation householdInvitation)
        {
            if (ModelState.IsValid)       //This step will trigger at least 1 of 3 possible scenarios: timely acceptance, late acceptance or no response.
            {
                householdInvitation.Code = Guid.NewGuid();
                householdInvitation.Created = DateTime.Now;
                _context.Add(householdInvitation);
                await _context.SaveChangesAsync();
                var callbackUrl = Url.Action("AcceptedHouseholdInvitation", "HouseholdInvitations",
                    new { email = householdInvitation.RecipientName, code = householdInvitation.Code }, protocol: Request.Scheme);
                var emailBody = $"{householdInvitation.Body} <br/> Register and accept by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>" +
                    $"or if already register you can log in and use the following code <br /> Household Invitation Code: {householdInvitation.Code}";
                await _emailSender.SendEmailAsync(householdInvitation.RecipientName, householdInvitation.Subject, emailBody);
                return RedirectToAction("Details", "Households", new { id = householdInvitation.HouseholdId });
            }
            ViewData["HouseholdId"] = new SelectList(_context.Household, "Id", "Name", householdInvitation.HouseholdId);
            return View(householdInvitation);

        }

        public async Task<IActionResult> AcceptHouseholdInvitation(string email, string code)
        {

            //Step 1: Determine if the invitation is good
            var householdInvitation = _context.HouseholdInvitation.FirstOrDefault(i => i.Code.ToString() == code);
            if(householdInvitation == null)
            {
                return RedirectToAction("NotFound", new { email = email});
            }

            //Step 2: If found, determine whether it can be used: Check the IsValid flag. 
            if (!householdInvitation.IsValid)
            {
                TempData["Message"] = $"Your invitation has been denied for the following reason:";
                TempData["Message"] = $"<br /> It has been marked as Invalid";

                return RedirectToAction("InvitationDenied", new { email = email });
            }

            //Ste 3: Compare expiration date against current date.
            if (DateTime.Now > householdInvitation.Expires)
            {
                householdInvitation.IsValid = false;
                await _context.SaveChangesAsync();

                TempData["Message"] = $"Your invitation has been denied for the following reason:";
                TempData["Message"] = $"<br /> The invitation expired on {householdInvitation.Expires.ToString("MMM dd, yyyy")}";

                return RedirectToAction("Expired", new { email = email });

            }

            //Step 4: I am to presume the invitation is good
            //Expectation: 1) Mark the invitation as Accepted  2) Mark the invitation as IsValid
            // 3) Send user to custom registration 
            householdInvitation.Accepted = true;
            householdInvitation.IsValid = false;
            await _context.SaveChangesAsync();

            return RedirectToAction("Special Registration", new { code = householdInvitation.Code });

        }

        // GET: HouseholdInvitations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdInvitation = await _context.HouseholdInvitation.FindAsync(id);
            if (householdInvitation == null)
            {
                return NotFound();
            }
            return View(householdInvitation);
        }

        // POST: HouseholdInvitations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HouseholdId,Created,Expires,Accepted,IsValid,RecipientName,Subject,Body,RoleName,Code")] HouseholdInvitation householdInvitation)
        {
            if (id != householdInvitation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(householdInvitation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdInvitationExists(householdInvitation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(householdInvitation);
        }

        // GET: HouseholdInvitations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdInvitation = await _context.HouseholdInvitation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdInvitation == null)
            {
                return NotFound();
            }

            return View(householdInvitation);
        }

        // POST: HouseholdInvitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var householdInvitation = await _context.HouseholdInvitation.FindAsync(id);
            _context.HouseholdInvitation.Remove(householdInvitation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseholdInvitationExists(int id)
        {
            return _context.HouseholdInvitation.Any(e => e.Id == id);
        }

  
    }
}
