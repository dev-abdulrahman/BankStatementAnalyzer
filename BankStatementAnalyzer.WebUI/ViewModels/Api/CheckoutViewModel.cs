using System;
using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel()
        {
            NewCanDetails = new List<NewCanDetails>();
        }

        public int OrderId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal WalletPoints { get; set; }

        public decimal PayableAmount { get; set; }

        public string CustomerId { get; set; }

        public int AddressId { get; set; }

        public string CompanyKey { get; set; }

        public bool IsReturnAble { get; set; }

        public int NoOfCans { get; set; }

        public int TimeSlotId { get; set; }

        public BankStatementAnalyzer.Models.PaymentType PaymentType { get; set; }

        public BankStatementAnalyzer.Models.OrderType OrderType { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public List<NewCanDetails> NewCanDetails { get; set; }
    }

    public class NewCanDetails
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}