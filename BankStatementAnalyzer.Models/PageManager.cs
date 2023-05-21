using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankStatementAnalyzer.Models
{
    public class PageManager : BaseModel
    {
        [StringLength(100)]
        [Required]
        [Display(Name = "Page Name")]
        public string PageName { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Page Title")]
        public string PageTitle { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Page Meta Keyword")]
        public string PageMetaKeyword { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Page Meta Description")]
        public string PageMetaDescription { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Page Description")]
        public string PageDescription { get; set; }
    }
}