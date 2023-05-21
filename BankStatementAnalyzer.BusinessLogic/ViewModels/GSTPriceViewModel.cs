using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.BusinessLogic.ViewModels
{
    public class GSTPriceViewModel
    {
        public decimal FinalPrice { get; set; }

        public decimal CGSTPercentage { get; set; }

        public decimal SGSTPercentage { get; set; }

        public decimal CGSTAmount { get; set; }

        public decimal SGSTAmount { get; set; }

    }
}
