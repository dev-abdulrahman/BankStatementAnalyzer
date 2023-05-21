using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class CarouselApiVM
    {
        public int Id { get; set; }

        public bool Status { get; set; }

        public string ImageUrl { get; set; }

        public int ClassifiedId { get; set; }
    }
}