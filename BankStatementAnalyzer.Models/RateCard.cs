using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class RateCard : BaseModel
    {
        [ForeignKey("Cloth")]
        public int ClothId { get; set; }
        public virtual Cloths Cloth { get; set; }

        [ForeignKey("ServiceType")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public decimal UrgetRate { get; set; }

        public decimal NormalRate { get; set; }

        [NotMapped]
        public int DDCloth { get; set; }
    }
}