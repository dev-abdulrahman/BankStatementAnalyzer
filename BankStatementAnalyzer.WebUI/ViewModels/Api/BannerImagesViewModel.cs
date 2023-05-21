using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class BannerImagesViewModel
    {
        public int Id { get; set; }

        public string ImageURL { get; set; }

        public int BannerId { get; set; }
    }
}