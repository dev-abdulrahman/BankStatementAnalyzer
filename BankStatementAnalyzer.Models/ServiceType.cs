using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class ServiceType : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}