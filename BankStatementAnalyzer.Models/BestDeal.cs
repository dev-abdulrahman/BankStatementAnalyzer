using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class BestDeal : BaseModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ClassifiedId { get; set; }

        public int CategoryId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[\w-\.]+@([\w-]+\.)+[\w-]+")]
        public string Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
    }
}
