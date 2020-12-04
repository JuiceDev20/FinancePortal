using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePortal.Models
{
    public class HouseholdAttachment
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FileName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }

        [StringLength(300, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string ContentType { get; set; }

        [StringLength(4000)]
        public byte[] FileData { get; set; }
   

    }
}
