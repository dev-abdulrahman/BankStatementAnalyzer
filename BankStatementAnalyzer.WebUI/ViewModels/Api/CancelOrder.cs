namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class CancelOrder
    {
        public string CustomerId { get; set; }

        public int OrderId { get; set; }

        public string CompanyKey { get; set; }
    }
}