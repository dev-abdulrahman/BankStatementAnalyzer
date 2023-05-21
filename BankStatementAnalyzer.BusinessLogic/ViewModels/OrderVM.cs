using System;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.ViewModels
{
    public class OrderVM
    {
        public OrderVM()
        {
            Address = new AddressVM();
            Customer = new CustomerVM();
            Order = new Models.Order();
            OrderSummary = new List<OrderSummary>();
            NewCanSummary = new List<NewCanSummary>();
        }

        public Models.Order Order { get; set; }

        public string Currency { get; set; }

        public string RedirectAction { get; set; }

        public CustomerVM Customer { get; set; }

        public AddressVM Address { get; set; }

        public List<OrderSummary> OrderSummary { get; set; }

        public List<NewCanSummary> NewCanSummary { get; set; }

        public int OrderId { get; set; }

        public string OrderType { get; set; }

        public string OrderUniqueNumber { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentType { get; set; }

        public DateTime PlacedOn { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal WalletPoinst { get; set; }

        public decimal DeliveryCharge { get; set; }

        public decimal Coupon { get; set; }

        public string TimeSlot { get; set; }

        public int DeliveryBoyId { get; set; }

        public int VendorId { get; set; }

        public string Remark { get; set; }

        public string CreditOrAdvance { get; set; }

        public string PackageName { get; set; }
    }

    public class OrderSummary
    {
        public int Id { get; set; }

        public string Cloth { get; set; }

        public string Service { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPayable { get; set; }

        public bool IsUrgent { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }
    }

    public class NewCanSummary
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

    public class CustomerVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }
    }

    public class AddressVM
    {
        public int Id { get; set; }

        public string DeliveryAddress { get; set; }

        public string LandMark { get; set; }

        public string Flat { get; set; }

        public string Area { get; set; }

        public string Pincode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}