using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class DeliveryBoy : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //[RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter valid mobile number eg :- 9788898000")]
        [Required(ErrorMessage = "Please enter Mobile")]
        [Display(Name = "Mobile")]
        [Phone]
        [StringLength(15)]
        public string Number { get; set; }

        //[Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[\w-\.]+@([\w-]+\.)+[\w-]+")]
        public string Email { get; set; }

        [Required]
        [StringLength(1024)]
        public string Address { get; set; }

        [StringLength(15)]
        //[Required]
        [DisplayName("Vehicle No.")]
        //[RegularExpression(@"^[A-Z]{2}[0-9]{1,2}(?:[A-Z]*)?[0-9]{4}$", ErrorMessage = "Please enter valid registration number for eg. MH11AA1234")]
        public string VehicleNumber { get; set; }
    }
}