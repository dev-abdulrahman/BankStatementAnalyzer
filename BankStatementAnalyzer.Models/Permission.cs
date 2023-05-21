using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Permission
    {
        public Permission()
        {
            this.Actions = new List<Action>();
            this.Roles = new List<Role>();
        }

        [Key]
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Feature")]
        public string Feature { get; set; }

        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
