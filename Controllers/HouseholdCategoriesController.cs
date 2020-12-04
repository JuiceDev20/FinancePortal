﻿using System;
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
    public class HouseholdCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseholdCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HouseholdCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.HouseholdCategory.ToListAsync());
        }

        // GET: HouseholdCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdCategory = await _context.HouseholdCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdCategory == null)
            {
                return NotFound();
            }

            return View(householdCategory);
        }

        // GET: HouseholdCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HouseholdCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HouseholdId,Name,Description")] HouseholdCategory householdCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(householdCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(householdCategory);
        }

        // GET: HouseholdCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdCategory = await _context.HouseholdCategory.FindAsync(id);
            if (householdCategory == null)
            {
                return NotFound();
            }
            return View(householdCategory);
        }

        // POST: HouseholdCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HouseholdId,Name,Description")] HouseholdCategory householdCategory)
        {
            if (id != householdCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(householdCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdCategoryExists(householdCategory.Id))
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
            return View(householdCategory);
        }

        // GET: HouseholdCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var householdCategory = await _context.HouseholdCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (householdCategory == null)
            {
                return NotFound();
            }

            return View(householdCategory);
        }

        // POST: HouseholdCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var householdCategory = await _context.HouseholdCategory.FindAsync(id);
            _context.HouseholdCategory.Remove(householdCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseholdCategoryExists(int id)
        {
            return _context.HouseholdCategory.Any(e => e.Id == id);
        }
    }
}
