using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancePortal.Data;
using FinancePortal.Models;

namespace FinancePortal.Controllers
{
    public class HouseholdInvitationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseholdInvitationsController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("Id,HouseholdId,Created,Expires,Accepted,IsValid,RecipientName,Subject,Body,RoleName,Code")] HouseholdInvitation householdInvitation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(householdInvitation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(householdInvitation);
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
