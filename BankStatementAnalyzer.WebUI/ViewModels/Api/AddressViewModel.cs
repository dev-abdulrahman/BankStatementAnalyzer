using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Flat { get; set; }

        public string Area { get; set; }

        public string LandMark { get; set; }

        public string DeliveryAddress { get; set; }

        public string CustomerID { get; set; }

        public string CompanyKey { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Pincode { get; set; }

        public AddressType AddressType { get; set; }
    }
}