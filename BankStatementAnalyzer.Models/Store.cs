using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public enum StoreType { InHouse, Distributor }

    public class Store : BaseModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(1024)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(15)]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter valid mobile number eg :- 9788898000")]
        //[Required(ErrorMessage = "Please enter Mobile")]
        [Display(Name = "Mobile")]
        [Phone]
        public string PhoneNumber { get; set; }

        public StoreType StoreType { get; set; }

        [Required]
        [StringLength(6)]
        public string Pincode { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public bool IsInstaServiceProvided { get; set; }
    }
}