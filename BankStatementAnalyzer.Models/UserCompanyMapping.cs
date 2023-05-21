using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class UserCompanyMapping
    {
        [Key]
        public int ID { get; set; }

        public int UserId { get; set; }

        public int CompanyId { get; set; }
    }
}