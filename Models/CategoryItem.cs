using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancePortal.Models
{
    public class CategoryItem
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public int TargetAmount { get; set; }   

        [Column(TypeName = "decimal(6, 2)")]
        public int ActualAmount { get; set; }   

    }
}
