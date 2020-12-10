using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancePortal.Models
{
    public class HouseholdBankAccount
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }


        public string FPUserId { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(35)]
        public int AccountType { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal StartingBalance { get; set; }   

        [Column(TypeName = "decimal(10, 2)")]
        public decimal CurrentBalance { get; set; }

        public FPUser FPUser { get; set; }

        public Household Household { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
    }
}
