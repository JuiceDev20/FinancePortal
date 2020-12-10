using FinancePortal.Data;
using FinancePortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FinancePortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<FPUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<FPUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _context.Users.FindAsync(userId);
            if (user.HouseholdId == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Dashboard", "Households");
            }

            
        }

        [AllowAnonymous]
        public IActionResult Landing()
        {
            return View();
        }


        //public IActionResult Dashboard()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
