namespace BankStatementAnalyzer.Models
{
    public class OrderVendorMapping : BaseModel
    {
        public int OrderId { get; set; }

        public int VendorId { get; set; }
    }
}