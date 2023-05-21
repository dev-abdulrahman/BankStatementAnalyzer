using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class OrderList
    {
        public int OrderID { get; set; }

        public int ServiceTypeID { get; set; }

        public int ClothsID { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public bool IsUrgent { get; set; }

        public decimal TotalPayable { get; set; }
    }
}