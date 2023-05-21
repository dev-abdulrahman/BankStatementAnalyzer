using System;

namespace BankStatementAnalyzer.SuperAdmin.ViewModels
{
    public class ExportOrdersViewModel
    {
        public string ReferenceNo { get; set; } 

        public string ConsigneeName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Pincode { get; set; }

        public string Mobile { get; set; }

        public string Weight { get; set; }

        public string PaymentOption { get; set; }

        public decimal PackageAmount { get; set; }

        public decimal SubAmount { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public string ProductToBeShipped { get; set; }

        public string ReturnAddress { get; set; }

        public string ReturnPincode { get; set; }

        public string SellerName { get; set; }

        public string SellerAddress { get; set; }

        public string SellerCSTNo { get; set; }

        public string SellerTinNo { get; set; }

        public string IvoiceNo { get; set; }

        public string InvoiceDate { get; set; }

        public string Quantity { get; set; }

        public string CommodityValue { get; set; }

        public string TaxValue { get; set; }

        public string Category { get; set; }

        public string SellerGstNumber { get; set; }

        public string HSNNumber { get; set; }

        public string ReturnReason { get; set; }

        public string VendorPickUpLocation { get; set; }

        public string EWBN { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}