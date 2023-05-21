using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class ImageCategory : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}