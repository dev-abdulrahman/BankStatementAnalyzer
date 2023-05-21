namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class OrderCreate
    {
        public string CustomerId { get; set; }

        public int? AddressId { get; set; }

        public int OrderTypeId { get; set; }

        public int? CouponId { get; set; }

        public int? PackageId { get; set; }

        public bool IsUrgent { get; set; }

        public decimal PackageAmount { get; set; }
    }
}