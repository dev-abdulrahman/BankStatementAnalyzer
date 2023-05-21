using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class Category : BaseModel
    {
        public int IconId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public bool IsSpecialCategory { get; set; }

        public virtual List<Banners> Banners { get; set; }
    }
}