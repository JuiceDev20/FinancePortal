using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePortal.Models.View_Models
{
    public class HouseholdViewModel
    {
        public HouseholdViewModel()
        {
            HouseholdBankAccounts = new HashSet<HouseholdBankAccount>();
            HouseholdCategories = new HashSet<HouseholdCategory>();
            CategoryItems = new HashSet<CategoryItem>();
            HouseholdNotifications = new HashSet<HouseholdNotification>();
            Transactions = new HashSet<Transaction>();

        }
        public int HouseholdId { get; set; }

        public Household Household { get; set; }

        public HouseholdBankAccount HouseholdBankAccount { get; set; } //If using a modal

        public virtual ICollection<HouseholdBankAccount> HouseholdBankAccounts { get; set; }

        public HouseholdCategory HouseholdCategory { get; set; }   //If using a modal

        public virtual ICollection<HouseholdCategory> HouseholdCategories { get; set; }

        public CategoryItem CategoryItem { get; set; }   //If using a modal

        public virtual ICollection<CategoryItem> CategoryItems { get; set; }

        public HouseholdNotification HouseholdNotification { get; set; }

        public virtual ICollection<HouseholdNotification> HouseholdNotifications { get; set; }

        public Transaction Transaction { get; set; }  //If using a modal

        public virtual ICollection<Transaction> Transactions { get; set; }


    }
}
