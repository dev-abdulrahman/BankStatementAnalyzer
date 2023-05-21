using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class AppMessage : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Key { get; set; }

        [Required]
        [StringLength(2048)]
        public string Message { get; set; }
    }
}