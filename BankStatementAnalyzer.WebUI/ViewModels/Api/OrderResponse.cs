using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class OrderResponse
    {
        public OrderResponse()
        {
            AddressId = new OrderAddressVM();
            Package = new OrderPackageVM();
            OrderDetails = new List<OrderDetailVM>();
        }

        public int Id { get; set; }

        public string OrderType { get; set; }

        public int OrderTypeId { get; set; }

        public OrderAddressVM AddressId { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal TotalPayable { get; set; }

        public OrderPackageVM Package { get; set; }

        public string Remark { get; set; }

        public List<OrderDetailVM> OrderDetails { get; set; }
    }

    public class OrderDetailVM
    {
        public OrderDetailVM()
        {
            OrderHistory = new List<OrderVM>();
            OrderEstimate = new List<OrderVM>();
        }

        public int OrderId { get; set; }

        public int Id { get; set; }

        public string Service { get; set; }

        public string Cloth { get; set; }

        public int Quantity { get; set; }

        public bool IsUrgent { get; set; }

        public decimal Price { get; set; }

        public List<OrderVM> OrderHistory { get; set; }

        public List<OrderVM> OrderEstimate { get; set; }
    }

    public class OrderVM
    {
        public DateTime CreatedOn { get; set; }

        public string Status { get; set; }
    }

    public class OrderAddressVM
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

    public class OrderPackageVM
    {
        public OrderPackageVM()
        {
            Description = new List<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Rate { get; set; }

        public string Dimension { get; set; }

        public bool IsUrgent { get; set; }

        public decimal UrgentRate { get; set; }

        public List<string> Description { get; set; }
    }
}