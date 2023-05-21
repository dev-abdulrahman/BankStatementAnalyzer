using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class CustomerNotificationViewModel
    {
        public int NotificationId { get; set; }

        public int CustomerId { get; set; }

        public bool IsViewed { get; set; }
    }
}