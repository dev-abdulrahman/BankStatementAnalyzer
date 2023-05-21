using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class BannerImages : BaseModel
    {
        public string ImageURL { get; set; }

        public int BannerId { get; set; }
    }
}
