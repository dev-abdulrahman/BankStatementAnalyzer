namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class MapPayment
    {
        public int OrderID { get; set; }

        public string PaymentID { get; set; }

        public bool Success { get; set; }

        public string Signature { get; set; }

        public string PaymentOrderID { get; set; }

        public bool IsPaymentSuccess { get; set; }

        public int PaymentErrorCode { get; set; }

        public string PaymentErrorMsg { get; set; }

        public string CompanyKey { get; set; }
    }
}