using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            CategoryItems = new List<CategoryItem>();
            HouseholdNotifications = new List<HouseholdNotification>();
            Transactions = new List<Transaction>();

        }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TargetAmount { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ActualAmount { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal StartingBalance { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal CurrentBalance { get; set; }


        public int HouseholdId { get; set; }

        public Household Household { get; set; }

        public FPUser FPUser { get; set; }

        public HouseholdBankAccount HouseholdBankAccount { get; set; } //If using a modal

        public List<HouseholdBankAccount> HouseholdBankAccounts { get; set; }

        public HouseholdCategory HouseholdCategory { get; set; }  //If using a modal

        public int HouseholdCategoryId { get; set; }

        public HouseholdInvitation HouseholdInvitation { get; set; } = new HouseholdInvitation(); 

        public List<HouseholdCategory> HouseholdCategories { get; set; }

        public CategoryItem CategoryItem { get; set; }   //If using a modal

        public List<CategoryItem> CategoryItems { get; set; }

        public HouseholdNotification HouseholdNotification { get; set; }

        public List<HouseholdNotification> HouseholdNotifications { get; set; }

        public Transaction Transaction { get; set; }  //If using a modal

        public List<Transaction> Transactions { get; set; }


    }
}
