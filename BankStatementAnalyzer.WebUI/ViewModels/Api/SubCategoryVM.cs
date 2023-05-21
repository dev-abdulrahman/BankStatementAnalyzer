using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class SubCategoryVM
    {
        public int Id { get; set; }

        public bool Status { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int CategoryID { get; set; }

        public bool IsSpecialCategory { get; set; }
    }
}