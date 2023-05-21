using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankStatementAnalyzer.Models
{
    public class Email : BaseModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Email For")]
        public string EmailFor { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Email Subject")]
        public string EmailSubject { get; set; }

        [AllowHtml]
        public string Description { get; set; }
    }
}