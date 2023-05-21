using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class PushNotification : BaseModel
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string CustomerName { get; set; }

        public int ClassifiedId { get; set; }

        public string ImageURL { get; set; }
    }
}