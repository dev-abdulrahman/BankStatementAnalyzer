using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels.ReportVM
{
    public class ProfitAndLossReportFilterModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}