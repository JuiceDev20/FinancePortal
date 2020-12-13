using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancePortal.Data;
using FinancePortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancePortal.Controllers
{
    public class HouseholdBankAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FPUser> _userManager;

        public HouseholdBankAccountsController(ApplicationDbContext context, UserManager<FPUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HouseholdBankAccounts
        public async Task<IActionResult> Index(int? id)
        {
            var userId = _userManager.GetUserId(User);
            var user = _context.Users.Find(userId);

            return View(await _context.HouseholdBankAccount.Where(u => u.HouseholdId == user.HouseholdId).ToListAsync());
        }

        // GET: HouseholdBankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdBankAccount = await _context.HouseholdBankAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdBankAccount == null)
            {
                return NotFound();
            }

            return View(householdBankAccount);
        }

        // GET: HouseholdBankAccounts/Create
        public async Task<IActionResult> Create()
        {
            var data = new HouseholdBankAccount
            {
                HouseholdId = (int)(await _userManager.GetUserAsync(User)).HouseholdId
            };
            //ViewData["HouseholdId"] = (await _userManager.GetUserAsync(User)).HouseholdId;
            return View(data);
        }

        // POST: HouseholdBankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseholdId,Name,AccountType,StartingBalance")] HouseholdBankAccount householdBankAccount)
        {
            if (ModelState.IsValid)
            {
                householdBankAccount.FPUserId = _userManager.GetUserId(User);
                householdBankAccount.CurrentBalance = householdBankAccount.StartingBalance;
                _context.Add(householdBankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Households");
            }
            ViewData["HouseholdId"] = new SelectList(_context.Household, "Id", "Id", householdBankAccount.HouseholdId);
            ViewData["FPUserId"] = new SelectList(_context.Users, "Id", "Name", householdBankAccount.FPUserId);

            return View(householdBankAccount);
        }

        // GET: HouseholdBankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.HouseholdBankAccount.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            model.FPUserId = userId;
            return View(model);
        }

        // POST: HouseholdBankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FPUserId,HouseholdId,Name,AccountType,StartingBalance,CurrentBalance")] HouseholdBankAccount model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdBankAccountExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Dashboard", "Households");
            }
            return View(model);
        }

        // GET: HouseholdBankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdBankAccount = await _context.HouseholdBankAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdBankAccount == null)
            {
                return NotFound();
            }

            return View(householdBankAccount);
        }

        // POST: HouseholdBankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var householdBankAccount = await _context.HouseholdBankAccount.FindAsync(id);
            _context.HouseholdBankAccount.Remove(householdBankAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard", "Households");
        }

        private bool HouseholdBankAccountExists(int id)
        {
            return _context.HouseholdBankAccount.Any(e => e.Id == id);
        }
    }
}
