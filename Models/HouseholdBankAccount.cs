using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePortal.Models
{
    public class HouseholdBankAccount
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string PortalUserId { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(35)]
        public int AccountType { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal StartingBalance { get; set; }   

        [Column(TypeName = "decimal(10, 2)")]
        public decimal CurrentBalance { get; set; }   

    }
}
