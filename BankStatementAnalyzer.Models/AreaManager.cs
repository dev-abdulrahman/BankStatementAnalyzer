using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class AreaManager : BaseModel
    {
        [ForeignKey("City")]
        [Display(Name = "City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Pincode { get; set; }
    }
}