using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class UnitOfMeasure : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}