﻿using FinancePortal.Data;
using FinancePortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinancePortal.Controllers
{

    public class ChartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FPUser> _userManager;


        public ChartsController(ApplicationDbContext context, UserManager<FPUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public JsonResult CurrentBalanceChart()
        {
            var result = new List<ChartModel>();
            
            foreach (var account in _context.HouseholdBankAccount.ToList())
            {
                var vm = new ChartModel
                {
                    Labels = account.Name, 
                    Values = account.CurrentBalance
                };
                result.Add(vm);
            }
            return Json(result);
        }

        public JsonResult TransactionChart()
        {
            var result = new List<ChartModel>();

            foreach (var transaction in _context.HouseholdBankAccount.Include(u => u.FPUser).ToList())
            {
                var vm = new ChartModel
                {
                    Labels = transaction.FPUser.FullName,
                    Values = transaction.CurrentBalance
                };
                result.Add(vm);
            }
            return Json(result);
        }

    }
}





