using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Street : BaseModel
    {
        [Required]
        [StringLength(2024)]
        public string Name { get; set; }

        [ForeignKey("AreaManager")]
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public virtual AreaManager AreaManager { get; set; }
    }
}