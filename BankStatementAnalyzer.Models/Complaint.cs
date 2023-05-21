using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class Complaint : BaseModel
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}