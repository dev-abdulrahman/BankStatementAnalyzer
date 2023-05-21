using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Banners : BaseModel
    {
        public Banners()
        {
            //this.BannerImages = new List<BannerImages>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        [StringLength(300)]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        public int LikeCount { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public bool IsLiked { get; set; }
    }
}
