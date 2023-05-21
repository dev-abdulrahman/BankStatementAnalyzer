using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Company
    {
        public Company()
        {
            CreatedOn = DateTime.Now;
            Status = true;
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Key { get; set; }

        public bool Status { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }

        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ModifiedOn { get; set; }

        [ScaffoldColumn(false)]
        public string ModifiedBy { get; set; }

        [NotMapped]
        public bool CreateAnother { get; set; }
    }
}
