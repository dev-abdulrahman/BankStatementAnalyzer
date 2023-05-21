using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class CustomerLikes : BaseModel
    {
        public int CustomerId { get; set; }

        public int BannerId { get; set; }

        [NotMapped]
        public bool IsLiked { get; set; }
    }
}
