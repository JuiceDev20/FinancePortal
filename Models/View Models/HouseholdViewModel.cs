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
            HouseholdBankAccounts = new List<HouseholdBankAccount>();
            HouseholdCategories = new List<HouseholdCategory>();
            HouseholdNotifications = new List<HouseholdNotification>();
            Transactions = new List<Transaction>();

        }
        public int HouseholdId { get; set; }

        public Household Household { get; set; }

        public FPUser FPUser { get; set; }

        public HouseholdBankAccount HouseholdBankAccount { get; set; } //If using a modal

        public List<HouseholdBankAccount> HouseholdBankAccounts { get; set; }

        public HouseholdCategory HouseholdCategory { get; set; }  //If using a modal


        public List<HouseholdCategory> HouseholdCategories { get; set; }

        public CategoryItem CategoryItem { get; set; }   //If using a modal

        public List<CategoryItem> CategoryItems { get; set; }

        public HouseholdNotification HouseholdNotification { get; set; }

        public List<HouseholdNotification> HouseholdNotifications { get; set; }

        public Transaction Transaction { get; set; }  //If using a modal

        public List<Transaction> Transactions { get; set; }


    }
}
