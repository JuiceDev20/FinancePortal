using System;
using System.ComponentModel.DataAnnotations;

namespace FinancePortal.Models
{
    public class HouseholdInvitation
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Expires { get; set; }

        public bool Accepted { get; set; }

        public bool IsValid { get; set; }

        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Email { get; set; }

        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string EmailTo { get; set; }

        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Subject { get; set; }

        [StringLength(300, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Body { get; set; }

        public int RoleName { get; set; }

        public Guid Code { get; set; } = Guid.NewGuid();

        public Household Household { get; set; } 
    }
}
