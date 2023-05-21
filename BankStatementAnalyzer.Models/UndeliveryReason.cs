using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class UndeliveryReason : BaseModel
    {
        [Required]
        public string Reason { get; set; }
    }
}