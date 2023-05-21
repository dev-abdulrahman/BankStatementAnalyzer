using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class BannersViewModel
    {
        public BannersViewModel()
        {
            this.Images = new List<string>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public List<string> Images { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public int LikeCount { get; set; }

        public bool IsLiked { get; set; }
    }
}