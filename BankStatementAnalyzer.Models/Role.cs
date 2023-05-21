using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class Role
    {
        public Role()
        {
            this.Permissions = new List<Permission>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Default Controller")]
        public string DefaultController { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Default Action")]
        public string DefaultAction { get; set; }

        public virtual List<Permission> Permissions { get; set; }

        public virtual List<User> Users { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
