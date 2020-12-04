using FinancePortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancePortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<FPUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FinancePortal.Models.Household> Household { get; set; }
        public DbSet<FinancePortal.Models.HouseholdAttachment> HouseholdAttachment { get; set; }
        public DbSet<FinancePortal.Models.HouseholdBankAccount> HouseholdBankAccount { get; set; }
        public DbSet<FinancePortal.Models.HouseholdCategory> HouseholdCategory { get; set; }
        public DbSet<FinancePortal.Models.HouseholdInvitation> HouseholdInvitation { get; set; }
        public DbSet<FinancePortal.Models.CategoryItem> CategoryItem { get; set; }
        public DbSet<FinancePortal.Models.HouseholdNotification> HouseholdNotification { get; set; }
        public DbSet<FinancePortal.Models.Transaction> Transaction { get; set; }
    }
}
