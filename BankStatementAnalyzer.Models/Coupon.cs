using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Coupon : BaseModel
    {
        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        [StringLength(15)]
        public string Code { get; set; }

        public DiscountType DiscountType { get; set; }

        public decimal MinTotal { get; set; }

        public int Redemption { get; set; }

        public int CustomerRedemption { get; set; }

        public string Description { get; set; }

        public decimal Discount { get; set; }

        [NotMapped]
        public int PackageId { get; set; }
    }
}
