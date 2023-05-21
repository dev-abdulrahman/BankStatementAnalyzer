using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public enum FAQType
    {
        Customer,

        [Display(Name = "Delivery boy")]
        DeliveryBoy
    }

    public class SupportAndFAQ : BaseModel
    {
        public SupportAndFAQ()
        {
            Status = true;
        }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(2400)]
        public string Description { get; set; }

        public FAQType FAQType { get; set; }
    }
}