using System;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class CustomerCreditHistory
    {
        [Key]
        public int Id { get; set; }

        public int CustomerCreditMappingId { get; set; }

        public decimal AmountPaid { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public string CreatedBy { get; set; }
    }
}