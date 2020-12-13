using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancePortal.Models
{
    public class HouseholdCategory
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        public Household Household { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TargetAmount { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ActualAmount { get; set; }


        public ICollection<CategoryItem> CategoryItems { get; set; }
        public HouseholdCategory()
        {
            CategoryItems = new HashSet<CategoryItem>();
        }

    }
}
