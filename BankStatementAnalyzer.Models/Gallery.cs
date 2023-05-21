using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Gallery : BaseModel
    {
        public string Title { get; set; }

        [ForeignKey("ImageCategory")]
        [Display(Name = "Image Category")]
        public int ImageCategoryId { get; set; }
        public virtual ImageCategory ImageCategory { get; set; }

        [Display(Name = "Image")]
        [ForeignKey("File")]
        public int FileId { get; set; }
        public virtual File File { get; set; }
    }
}