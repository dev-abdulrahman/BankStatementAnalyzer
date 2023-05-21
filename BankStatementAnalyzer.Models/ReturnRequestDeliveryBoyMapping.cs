namespace BankStatementAnalyzer.Models
{
    public class ReturnRequestDeliveryBoyMapping : BaseModel
    {
        public int ReturnRequestId { get; set; }

        public int DeliveryBoyId { get; set; }
    }
}