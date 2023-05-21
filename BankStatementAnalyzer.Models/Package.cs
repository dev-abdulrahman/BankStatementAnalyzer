using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Package : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Rate { get; set; }

        public decimal UrgentRate { get; set; }

        [StringLength(100)]
        [Required]
        public string Diemension { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Heading { get; set; }

        public int BagCount { get; set; }

        [NotMapped]
        public bool CanDelete { get; set; }
    }
}