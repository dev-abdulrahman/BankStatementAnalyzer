using System;

namespace BankStatementAnalyzer.Models
{
    public enum Transactiontype { CR, DR }

    public class Wallet : BaseModel
    {
        public string CustomerId { get; set; }

        public string TransactionDetail { get; set; }

        public decimal Amount { get; set; }

        public Transactiontype Transactiontype { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}