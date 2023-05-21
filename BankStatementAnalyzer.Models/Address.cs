using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public enum AddressType { Home, Office, Other }

    public class Address : BaseModel
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Flat { get; set; }

        [StringLength(100)]
        public string Area { get; set; }

        [StringLength(100)]
        public string LandMark { get; set; }

        [StringLength(1024)]
        public string DeliveryAddress { get; set; }

        public int CustomerID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public AddressType AddressType { get; set; }

        public string Pincode { get; set; }
    }
}