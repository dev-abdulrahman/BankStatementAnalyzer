using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public int IconId { get; set; }

        public string Name { get; set; }

        public bool IsSpecialCategory { get; set; }

        public string IconColor { get; set; }

    }
}