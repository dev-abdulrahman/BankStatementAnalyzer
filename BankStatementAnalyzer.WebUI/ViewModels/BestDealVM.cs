using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class BestDealVM
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ClassifiedId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}