namespace BankStatementAnalyzer.Models
{
    public class OrderDelvieryBoyMapping : BaseModel
    {
        public int OrderId { get; set; }

        public int DeliveryBoyId { get; set; }

        public MappedFor MappedFor { get; set; }
    }

    public enum MappedFor { CustomerPickUp, VendorPickUp, CustomerDelivery }
}