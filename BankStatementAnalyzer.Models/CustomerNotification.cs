using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class CustomerNotification : BaseModel
    {
        public int NotificationId { get; set; }

        [ForeignKey("NotificationId")]
        public PushNotification Notification { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [NotMapped]
        public bool IsViewed { get; set; }
    }
}
