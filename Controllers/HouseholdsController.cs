﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancePortal.Data;
using FinancePortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FinancePortal.Enums;
using HouseholdRole = FinancePortal.Enums.HouseholdRole;
using Microsoft.Extensions.Logging;
using FinancePortal.Areas.Identity.Pages.Account;

namespace FinancePortal.Controllers
{
    public class HouseholdsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FPUser> _userManager;
        private readonly SignInManager<FPUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        public HouseholdsController(ApplicationDbContext context, UserManager<FPUser> userManage, SignInManager<FPUser> signInManager, ILogger<RegisterModel> logger)
        {
            _context = context;
            _userManager = userManage;
            _signInManager = signInManager;
            _logger = logger;
        }

        [Authorize(Roles = "HEAD, MEMBER")]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var houseId = user.HouseholdId;
            var hhId = (await _userManager.GetUserAsync(User)).HouseholdId;

            var household = await _context.Household
                .Include(h => h.Occupants)
                .ThenInclude(u => u.Transactions)
                .Include(h => h.Occupants)
                .Include(u => u.Categories)
                .ThenInclude(h => h.CategoryItems)
                .Include(h => h.Occupants)
                .ThenInclude(u => u.HouseholdBankAccounts)
                .FirstOrDefaultAsync(m => m.Id == hhId);
            if (household == null)
            {
                return NotFound();
            }
            return View(household);
        }

        [Authorize(Roles = "HEAD, MEMBER")]
        public async Task<IActionResult> Leave()
        {
            //Determine member's role in the house...
            //Step 1: Get my user record
            var user = await _userManager.GetUserAsync(User);
            var memberCount = _context.Users.Where(u => u.HouseholdId == user.HouseholdId).Count();

            if (User.IsInRole(nameof(HouseholdRole.HEAD)) && memberCount >1)
            {
                //Send rule reminder
                TempData["Message"] = "You cannot leave the Household until until all the members have left.";
                return RedirectToAction("Dashboard");
            }
            //Step 2: Remove the HouseholdId
            var householdIdMemento = user.HouseholdId;
            user.HouseholdId = null;
            await _context.SaveChangesAsync();

            //Step 3: Get the user's role
            var myRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            //Step 4: Remove from that role 
            await _userManager.RemoveFromRoleAsync(user, myRole);

            //Step 5: Refresh user login
            await _signInManager.RefreshSignInAsync(user);

            //Step 6: Redirect to the Lobby
            if(_context.Users.Where(u => u.HouseholdId == householdIdMemento).Count() == 0)
            {
                var household = await _context.Household.FindAsync(householdIdMemento);
                _context.Household.Remove(household);
                await _context.SaveChangesAsync();

            }
            //Step 7: Redirect to Lobby
            return RedirectToAction("Index", "Home");

        }

        // GET: Households
        public async Task<IActionResult> Index()
        {
            return View(await _context.Household.ToListAsync());
        }

        // GET: Households/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Household
                .FirstOrDefaultAsync(m => m.Id == id);
            if (household == null)
            {
                return NotFound();
            }

            return View(household);
        }

        // GET: Households/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Greeting")] Household household)
        {
            if (ModelState.IsValid)
            {
                household.Created = DateTime.Now;

                _context.Add(household);
                await _context.SaveChangesAsync();

                //var currentUser = await _userManager.GetUserAsync(User);
                var currentUser = await _context.Users.FindAsync(_userManager.GetUserId(User));
                await _userManager.AddToRoleAsync(currentUser, HouseholdRole.HEAD.ToString());
                currentUser.HouseholdId = household.Id;
                await _context.SaveChangesAsync();

                await _signInManager.RefreshSignInAsync(currentUser);

                return RedirectToAction("Dashboard", "Households", new { id = household.Id});
            }
            return View(household);
        }

        // GET: Households/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Household.FindAsync(id);
            if (household == null)
            {
                return NotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Greeting,Created")] Household household)
        {
            if (id != household.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(household);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdExists(household.Id))
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
            return View(household);
        }

        // GET: Households/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Household
                .FirstOrDefaultAsync(m => m.Id == id);
            if (household == null)
            {
                return NotFound();
            }

            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var household = await _context.Household.FindAsync(id);
            _context.Household.Remove(household);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseholdExists(int id)
        {
            return _context.Household.Any(e => e.Id == id);
        }
    }
}
