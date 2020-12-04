using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int CategryItemId { get; set; }

        public int HouseholdBankAccountId { get; set; }

        [StringLength(35)]
        public string PortalUserId { get; set; }

        public DateTimeOffset Created { get; set; }

        public string ContentType { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Memo { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Amount { get; set; }   

        public bool IsDeleted { get; set; }

    }
}
