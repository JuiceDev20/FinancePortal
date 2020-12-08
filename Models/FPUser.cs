using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancePortal.Models
{
    public class FPUser : IdentityUser
    {
        public int? HouseholdId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [Display(Name = "Avatar")]
        [NotMapped]
        [DataType(DataType.Upload)]

        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        public virtual ICollection<HouseholdBankAccount> HouseholdBankAccounts { get; set; } = new HashSet<HouseholdBankAccount>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();

    }
}
