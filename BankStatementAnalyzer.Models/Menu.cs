using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class Menu
    {
        public Menu()
        {
            this.Children = new List<Menu>();
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Route
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Controller))
                    return null;
                else
                    return string.Format("{0}.{1}.{2}", "ACTION", Controller, ActionName);
            }
        }

        public string ActionName { get; set; }

        public string Controller { get; set; }

        public string QueryString { get; set; }

        public int Order { get; set; }

        public int? ParentId { get; set; }

        [StringLength(100)]
        public string Icon { get; set; }

        [StringLength(100)]
        public string Separator { get; set; }

        public bool HideFromSystemAdministrator { get; set; }

        public virtual Menu Parent { get; set; }

        public virtual IList<Menu> Children { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(1000)]
        public string UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }
    }
}
