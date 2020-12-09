using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancePortal.Data;
using FinancePortal.Models;

namespace FinancePortal.Controllers
{
    public class HouseholdAttachmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseholdAttachmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HouseholdAttachments
        public async Task<IActionResult> Index()
        {
            return View(await _context.HouseholdAttachment.ToListAsync());
        }

        // GET: HouseholdAttachments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdAttachment = await _context.HouseholdAttachment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdAttachment == null)
            {
                return NotFound();
            }

            return View(householdAttachment);
        }

        // GET: HouseholdAttachments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HouseholdAttachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseholdId,FileName,Description,Created,ContentType,FileData")] HouseholdAttachment householdAttachment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(householdAttachment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Households");
            }
            return View(householdAttachment);
        }

        // GET: HouseholdAttachments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdAttachment = await _context.HouseholdAttachment.FindAsync(id);
            if (householdAttachment == null)
            {
                return NotFound();
            }
            return View(householdAttachment);
        }

        // POST: HouseholdAttachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HouseholdId,FileName,Description,Created,ContentType,FileData")] HouseholdAttachment householdAttachment)
        {
            if (id != householdAttachment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(householdAttachment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdAttachmentExists(householdAttachment.Id))
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
            return View(householdAttachment);
        }

        // GET: HouseholdAttachments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdAttachment = await _context.HouseholdAttachment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdAttachment == null)
            {
                return NotFound();
            }

            return View(householdAttachment);
        }

        // POST: HouseholdAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var householdAttachment = await _context.HouseholdAttachment.FindAsync(id);
            _context.HouseholdAttachment.Remove(householdAttachment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard", "Households");
        }

        private bool HouseholdAttachmentExists(int id)
        {
            return _context.HouseholdAttachment.Any(e => e.Id == id);
        }
    }
}
