using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels.ReportVM
{
    public class OrderReportVM
    {
        public int OrderNumber { get; set; }

        public string OrderType { get; set; }

        public string CustomerName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public string PaymentStatus { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal PaidByCustomer { get; set; }

        public decimal BalanceAmount { get; set; }

        public decimal AdvancedAmount { get; set; }

        public decimal Tip { get; set; }

        public decimal PaymentToVendor { get; set; }

        public string PaymentType { get; set; }

        public string Remark { get; set; }

    }
}