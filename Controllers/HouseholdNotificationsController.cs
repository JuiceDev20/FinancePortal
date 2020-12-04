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
    public class HouseholdNotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseholdNotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HouseholdNotifications
        public async Task<IActionResult> Index()
        {
            return View(await _context.HouseholdNotification.ToListAsync());
        }

        // GET: HouseholdNotifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdNotification = await _context.HouseholdNotification
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdNotification == null)
            {
                return NotFound();
            }

            return View(householdNotification);
        }

        // GET: HouseholdNotifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HouseholdNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HouseholdId,Created,Subject,Body,IsRead")] HouseholdNotification householdNotification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(householdNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(householdNotification);
        }

        // GET: HouseholdNotifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdNotification = await _context.HouseholdNotification.FindAsync(id);
            if (householdNotification == null)
            {
                return NotFound();
            }
            return View(householdNotification);
        }

        // POST: HouseholdNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HouseholdId,Created,Subject,Body,IsRead")] HouseholdNotification householdNotification)
        {
            if (id != householdNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(householdNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdNotificationExists(householdNotification.Id))
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
            return View(householdNotification);
        }

        // GET: HouseholdNotifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdNotification = await _context.HouseholdNotification
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdNotification == null)
            {
                return NotFound();
            }

            return View(householdNotification);
        }

        // POST: HouseholdNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var householdNotification = await _context.HouseholdNotification.FindAsync(id);
            _context.HouseholdNotification.Remove(householdNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseholdNotificationExists(int id)
        {
            return _context.HouseholdNotification.Any(e => e.Id == id);
        }
    }
}
