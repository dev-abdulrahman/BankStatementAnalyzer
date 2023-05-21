using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Payment : BaseModel
    {
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public virtual Order Order { get; set; }

        public string PaymentID { get; set; }

        public bool Success { get; set; }

        public string Signature { get; set; }

        public string PaymentOrderID { get; set; }

        public bool IsPaymentSuccess { get; set; }

        public int PaymentErrorCode { get; set; }

        public string PaymentErrorMsg { get; set; }
    }
}