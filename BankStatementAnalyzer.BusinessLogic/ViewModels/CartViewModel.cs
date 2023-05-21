using System;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartDetails = new List<CartDetail>();
        }

        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        public string OrderStatus { get; set; }

        public string CouponCode { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal WalletPoints { get; set; }

        public decimal CouponAmount { get; set; }

        public decimal PayableAmount { get; set; }

        public decimal DeliveryCharge { get; set; }

        public decimal NewCanPrice { get; set; }

        public string PaymentType { get; set; }

        public string OrderType { get; set; }

        public decimal NewCanTotal { get; set; }

        public DateTime? OrderedDate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public List<CartDetail> CartDetails { get; set; }
    }

    public class CartDetail
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public int SubCategoryId { get; set; }

        public string ImagePath { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public decimal Total { get; set; }

        public bool IsProductReturnAble { get; set; }

        public int NewCanQuantity { get; set; }

        public decimal NewCanTotal { get; set; }
    }
}