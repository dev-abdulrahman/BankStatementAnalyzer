using System;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.ViewModels
{
    public class ReturnCanVM
    {
        public ReturnCanVM()
        {
            Address = new AddressVM();
            Customer = new CustomerVM();
            ReturnCanSummary = new List<ReturnCanSummary>();
            DeliveryBoy = new DeliveryBoy();
        }

        public AddressVM Address { get; set; }

        public CustomerVM Customer { get; set; }

        public List<ReturnCanSummary> ReturnCanSummary { get; set; }

        public DateTime CreatedOn { get; set; }

        public int DeliveryBoyId { get; set; }

        public DeliveryBoy DeliveryBoy { get; set; }

        public string OrderStatus { get; set; }
    }

    public class ReturnCanSummary
    {
        public string Remark { get; set; }

        public int NoOfCans { get; set; }

        public string ReturnRequestType { get; set; }
    }

    public class DeliveryBoy
    {
        public string Name { get; set; }

        public string Number { get; set; }
    }
}