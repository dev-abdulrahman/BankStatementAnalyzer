using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class City : BaseModel
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        [Required]
        public string Code { get; set; }
    }
}