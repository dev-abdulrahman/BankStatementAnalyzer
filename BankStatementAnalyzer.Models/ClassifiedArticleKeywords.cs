using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class ClassifiedArticleKeywords
    {
        [Key]
        public int Id { get; set; }

        public string Keywords { get; set; }

        public int ClassifiedArticleId { get; set; }
    }
}
