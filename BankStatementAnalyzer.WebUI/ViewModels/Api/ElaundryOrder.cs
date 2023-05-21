using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class ElaundryOrder
    {
        public ElaundryOrder()
        {
            Address = new AddressVM();
            OrderDetails = new List<OrderDetail>();
        }

        public int Id { get; set; }

        public int OrderTypeId { get; set; }

        public AddressVM Address { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal TotalPayable { get; set; }

        public string Remark { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class AddressVM
    {
        public int Id { get; set; }

        public string Flat { get; set; }

        public string Area { get; set; }

        public string LandMark { get; set; }

        public string DeliveryAddress { get; set; }

        public int CustomerId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Pincode { get; set; }

        public AddressType AddressType { get; set; }
    }

    public class OrderDetail
    {
        public OrderDetail()
        {
            OrderHistory = new List<Order>();
        }

        public int OrderId { get; set; }

        public int Id { get; set; }

        public string Service { get; set; }

        public string Cloth { get; set; }

        public int Quantity { get; set; }

        public bool IsUrgent { get; set; }

        public decimal Price { get; set; }

        public List<Order> OrderHistory { get; set; }

        public Order[] OrderEstimate { get; set; }
    }

    public class Order
    {
        public DateTime CreatedOn { get; set; }

        public OrderStatus Status { get; set; }
    }
}