using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class ClassifiedArticleCategory
    {
        [Key]
        public int Id { get; set; }

        public int ClassifiedArticleId { get; set; }

        public int CategoryId { get; set; }
    }
}
