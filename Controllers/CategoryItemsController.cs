using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancePortal.Data;
using FinancePortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinancePortal.Controllers
{
    public class CategoryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FPUser> _userManager;

        public CategoryItemsController(ApplicationDbContext context, UserManager<FPUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CategoryItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoryItem.ToListAsync());
        }

        // GET: CategoryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // GET: CategoryItems/Create
        [Authorize(Roles = "HEAD, MEMBER")]
        public async Task<IActionResult> Create()
        {
            var householdId = (await _userManager.GetUserAsync(User)).HouseholdId;

            //Categories for my Household
            ViewData["CategoryId"] = new SelectList(_context.HouseholdCategory.Where(c => c.HouseholdId == householdId), "Id", "Name");

            return View();
        }

        // POST: CategoryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Description,TargetAmount")] CategoryItem categoryItem)
        {
            if (ModelState.IsValid)
            {
                var category = _context.HouseholdCategory.Find(categoryItem.CategoryId);
                category.TargetAmount += categoryItem.TargetAmount;
                _context.Add(categoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Households");
            }
            ViewData["CategoryId"] = new SelectList(_context.HouseholdCategory, "Id", "Name", categoryItem);
            return View(categoryItem);
        }

        // GET: CategoryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItem.FindAsync(id);
            if (categoryItem == null)
            {
                return NotFound();
            }
            return View(categoryItem);
        }

        // POST: CategoryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HouseholdId,Name,Description,TargetAmount")] CategoryItem categoryItem)
        {
            if (id != categoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryItemExists(categoryItem.Id))
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
            return View(categoryItem);
        }

        // GET: CategoryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // POST: CategoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryItem = await _context.CategoryItem.FindAsync(id);
            _context.CategoryItem.Remove(categoryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryItemExists(int id)
        {
            return _context.CategoryItem.Any(e => e.Id == id);
        }
    }
}
