using BankStatementAnalyzer.Models;
using System;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class WalletVM
    {
        public decimal Amount { get; set; }

        public Transactiontype Transactiontype { get; set; }

        public string TransactionDetail { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}