using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancePortal.Data;
using FinancePortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using FinancePortal.Services;

namespace FinancePortal.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FPUser> _userManager;
        private readonly INotificationService _notificationService;

        public TransactionsController(ApplicationDbContext context, UserManager<FPUser> userManager, INotificationService notificationService)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transaction.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public async Task<IActionResult> Create()
        {
            var householdId = (await _userManager.GetUserAsync(User)).HouseholdId;
            ViewData["HouseholdId"] = householdId;
            var myBankAccounts = _context.HouseholdBankAccount.Where(b => b.HouseholdId == householdId);
            ViewData["HouseholdBankAccountId"] = new SelectList(myBankAccounts, "Id", "Name");
            ViewData["CategoryItemId"] = new SelectList(_context.CategoryItem, "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryItemId,HouseholdBankAccountId,Type,Memo,Amount")] Transaction transaction, int OriginBankAccountId, int DestinationBankAccountId)
        {
            if (ModelState.IsValid)
            {
                transaction.FPUserId = _userManager.GetUserId(User);
                transaction.Created = DateTime.Now;
                //1.Deacrease the Current Balance
                decimal oldBalance = 0;
                HouseholdBankAccount account = new HouseholdBankAccount();

                if (transaction.Type == Enums.TransactionType.Deposit)
                {
                    account = await _context.HouseholdBankAccount.FindAsync(transaction.HouseholdBankAccountId);
                    oldBalance = account.CurrentBalance;
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();
                    account.CurrentBalance += transaction.Amount;
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                else if (transaction.Type == Enums.TransactionType.Transfer)
                {
                    //Withdrawal Transaction for transfer

                    transaction.HouseholdBankAccountId = OriginBankAccountId;
                    transaction.Type = Enums.TransactionType.Withdrawal;
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();

                    //Deposit Transaction for transfer

                    Transaction draftToAccount = new Transaction();
                    draftToAccount.HouseholdBankAccountId = DestinationBankAccountId;
                    draftToAccount.FPUserId = transaction.FPUserId;
                    draftToAccount.Type = Enums.TransactionType.Deposit;
                    draftToAccount.Memo = transaction.Memo;
                    draftToAccount.Created = transaction.Created;
                    draftToAccount.Amount = transaction.Amount;
                    _context.Add(draftToAccount);
                    await _context.SaveChangesAsync();

                    var originAccount = await _context.HouseholdBankAccount.FindAsync(OriginBankAccountId);
                    originAccount.CurrentBalance -= transaction.Amount;
                    _context.Update(originAccount);
                    await _context.SaveChangesAsync();

                    var receivingAccount = await _context.HouseholdBankAccount.FindAsync(DestinationBankAccountId);                
                    receivingAccount.CurrentBalance += transaction.Amount;                
                    _context.Update(receivingAccount);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    //Withdrawal or PointOfSale or ACH 
                    account.CurrentBalance -= transaction.Amount;
                    //2.Increase the Actual Amount of associated Category Item
                    var categoryItem = await _context.CategoryItem.FindAsync(transaction.CategoryItemId);
                    categoryItem.ActualAmount += transaction.Amount;
                    _context.Update(categoryItem);
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
               
                //3.Look for any needed modifications
                if (account.CurrentBalance < 0)
                {
                    TempData["OverDraft"] = "You're out of ca$h!!";
                    await _notificationService.NotifyOverdraftAsync(transaction, account, oldBalance);
                    return RedirectToAction("Dashboard", "Households", new { id = (await _userManager.GetUserAsync(User)).HouseholdId });
                }
                
                return RedirectToAction("Dashboard", "Households", new { id = (await _userManager.GetUserAsync(User)).HouseholdId });
            }
            ViewData["HouseholdBankAccountId"] = new SelectList(_context.HouseholdBankAccount, "Id", "Name", transaction.HouseholdBankAccountId);
            ViewData["CategoryItemId"] = new SelectList(_context.CategoryItem, "Id", "Name", transaction.CategoryItemId);
            ViewData["FPUserId"] = new SelectList(_context.Users, "Id", "Name", transaction.FPUserId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)         //Edit protocol ??
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategryItemId,HouseholdBankAccountId,FPUserId,Created,ContentType,Memo,Amount")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard", "Households");
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }
    }
}
