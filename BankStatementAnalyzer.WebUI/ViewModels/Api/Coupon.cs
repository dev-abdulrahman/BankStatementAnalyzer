namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal MinTotal { get; set; }

        public int Redemption { get; set; }

        public int CustomerRedemption { get; set; }

        public string Description { get; set; }

        public decimal Discount { get; set; }

        public string DiscountType { get; set; }
    }
}