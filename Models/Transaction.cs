using FinancePortal.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancePortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int? CategoryItemId { get; set; }

        public CategoryItem CategoryItem { get; set; }

        public int HouseholdBankAccountId { get; set; }

        public HouseholdBankAccount HouseholdBankAccount { get; set; }

        public TransactionType Type { get; set; }

        public string FPUserId { get; set; }

        public FPUser FPUser { get; set; }

        public DateTimeOffset Created { get; set; } 

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Memo { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }  

        public bool IsDeleted { get; set; }


    }
}
