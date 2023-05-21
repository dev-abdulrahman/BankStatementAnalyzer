using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class CustomerCreditMapping
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal? CreditAmount { get; set; }

        public decimal AdvancePayment { get; set; }

        public decimal AmountDue { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public string CreatedBy { get; set; }

        [NotMapped]
        public bool Status { get; set; }
    }
}