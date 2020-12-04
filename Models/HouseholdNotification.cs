using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePortal.Models
{
    public class HouseholdNotification
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        public DateTimeOffset Created { get; set; }

        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Subject { get; set; }

        [StringLength(300, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Body { get; set; }

        public bool IsRead { get; set; } 

    }
}
