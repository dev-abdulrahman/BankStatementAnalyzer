using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class SubCategory : BaseModel
    {
        [StringLength(100)]
        [Required]
        [Display(Name = "Subcategory Name")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Key { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [Display(Name = "Parent Category")]
        public int? ParentId { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
    }
}