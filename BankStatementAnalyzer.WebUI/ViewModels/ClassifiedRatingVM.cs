using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class ClassifiedRatingVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Review { get; set; }

        public int ClassifiedId { get; set; }

        public int CustomerId { get; set; }

        public int CategoryId { get; set; }

        public double Rating { get; set; }
    }
}