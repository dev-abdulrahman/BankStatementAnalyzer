using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class StyleTrait : BaseModel
    {
        [Required]
        [Display(Name = "StyleClass")]
        public int StyleClassId { get; set; }

        [Required]
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public string StyleClass { get; set; }
    }
}