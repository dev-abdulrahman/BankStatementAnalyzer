using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public enum ArticleAvailable
    {
        [Display(Name = "Available")]
        available = 1,

        [Display(Name = "Not Available")]
        not_available = 0
    }

    public class ClassifiedArticleVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Heading { get; set; }

        public string SubHeading { get; set; }

        public string ShortDescription { get; set; }

        public bool Status { get; set; }

        public IEnumerable<SelectListItem> SubCategories { get; set; }

        [Required]
        public List<int> SelectedSubCategory { get; set; }

        [StringLength(1024)]
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [StringLength(1024)]
        public string Address2 { get; set; }

        [Required]
        public ArticleAvailable Availability { get; set; }

        [Required(ErrorMessage = "Please enter Mobile")]
        [Display(Name = "Mobile")]
        [Phone]
        [StringLength(15)]
        public string ContactNo { get; set; }

        [Display(Name = "Upload Images")]
        //[Required(ErrorMessage = "Please choose file to upload.")]
        public HttpPostedFileBase[] Images { get; set; }

        [AllowHtml]
        [Required]
        public string Article { get; set; }

        public string Keywords { get; set; }

        public bool IsEditMode { get; set; } = false;
    }
}