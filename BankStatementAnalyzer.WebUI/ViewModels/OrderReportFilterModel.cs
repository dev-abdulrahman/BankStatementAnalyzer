using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class OrderReportFilterModel
    {
        public IEnumerable<SelectListItem> StatusList { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public OrderStatus SelectedStatus { get; set; }
    }
}