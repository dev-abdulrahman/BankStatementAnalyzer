using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class PushNotificationViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string CustomerName { get; set; }

        public int ClassifiedId { get; set; }

        public string ImageURL { get; set; }

        public bool IsViewed { get; set; }
    }
}