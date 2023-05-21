using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class OrderType : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Key { get; set; }
    }
}