using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class Action
    {
        public Action()
        {
            Permissions = new List<Permission>();
            this.CreatedOn = DateTime.Now;

        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Display Name")]
        [StringLength(100)]
        public string DisplayName { get; set; }

        public string Feature { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
