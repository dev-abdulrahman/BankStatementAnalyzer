using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class StyleClass : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }
    }
}