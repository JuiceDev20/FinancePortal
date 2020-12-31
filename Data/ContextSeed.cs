using FinancePortal.Enums;
using FinancePortal.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FinancePortal.Data
{

    public static class ContextSeed
    {
        public static async Task RunSeedMethod(
            RoleManager<IdentityRole> roleManager,
            UserManager<FPUser> userManager,
            ApplicationDbContext applicationDbContext)
        {
            await SeedRolesAsync(roleManager); 
        }


        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)

        {
            await roleManager.CreateAsync(new IdentityRole(HouseholdRole.ADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole(HouseholdRole.HEAD.ToString()));
            await roleManager.CreateAsync(new IdentityRole(HouseholdRole.MEMBER.ToString()));
        }
    }
}
