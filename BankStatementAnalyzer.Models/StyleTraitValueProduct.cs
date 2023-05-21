using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class StyleTraitValueProduct
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int StyleTraitValueId { get; set; }
    }
}