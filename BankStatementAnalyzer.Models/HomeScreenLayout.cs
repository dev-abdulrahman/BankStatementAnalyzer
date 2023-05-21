using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class HomeScreenLayout : BaseModel
    {
        public string IconId { get; set; }

        [ForeignKey("Color")]
        public int ColorId { get; set; }

        public Colors Color { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
