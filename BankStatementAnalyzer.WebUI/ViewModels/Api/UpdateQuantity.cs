namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class UpdateQuantity
    {
        public int OrderDetailId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public bool IsplusQuantity { get; set; }

        public string CompanyKey { get; set; }

        public string CustomerId { get; set; }
    }
}