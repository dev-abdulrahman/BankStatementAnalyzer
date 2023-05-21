using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class ClassifiedArticleApiVM
    {
        public ClassifiedArticleApiVM()
        {
            this.Categories = new List<string>();
            this.Images = new List<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Heading { get; set; }

        public string SubHeading { get; set; }

        public string ShortDescription { get; set; }

        public List<string> Categories { get; set; }

        public string Address { get; set; }

        public string Availability { get; set; }

        public string ContactNo { get; set; }

        public List<string> Images { get; set; }

        public string ArticleUrl { get; set; }
    }
}