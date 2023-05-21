using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class OrderVM
    {
        public OrderVM()
        {
            OrderDetail = new OrderDetail();
            Order = new Order();
        }

        public Order Order { get; set; }

        public OrderDetail OrderDetail { get; set; }

        public bool IsEdit { get; set; }

        public string RedirectAction { get; set; }

        public bool CreateAnother { get; set; }
    }
}