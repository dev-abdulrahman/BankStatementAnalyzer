using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class StyleTraitValue : BaseModel
    {
        [Required]
        [Display(Name = "StyleTrait")]
        public int StyleTraitId { get; set; }

        [Required]
        public string Value { get; set; }

        public string Description { get; set; }
    }
}