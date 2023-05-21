using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class OrderViewModel
    {
        public string CustomerId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int ProductBrandId { get; set; }

        public string CompanyKey { get; set; }

        public OrderType OrderType { get; set; }
    }
}