using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class Vendor : BaseModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(2000)]
        public string Address { get; set; }

        public VendorType VendorType { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public enum VendorType { InHouse, OutSide }
}