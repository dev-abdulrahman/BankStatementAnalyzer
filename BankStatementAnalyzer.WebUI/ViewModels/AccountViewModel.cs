using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class AccountViewModel
    {
        public class LoginViewModel
        {
            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class CompanyViewModel
        {
            public int Id { get; set; }
        }

        public class LocalPasswordModel
        {
            [Required]
            [RegularExpression("^(?=.*[a-zA-Z].*)(?=.*[!@#$&*])(?=.*[0-9].*)(?=.*[a-z].*[a-z].*).{8,}$", ErrorMessage = "Password must be 8 characters long and should contain at least 1 alphabet, 1 number and 1 special character.")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class RegisterViewModel
        {

            public string Corporate { get; set; }

            public string IRef { get; set; }

            public string CRef { get; set; }


            [Required]
            [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Mobile")]
            public string Mobile { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public bool IsCodeSent { get; set; }

            [Required(ErrorMessage = "Verification code is required.")]
            public int? Code { get; set; }

            [Required(ErrorMessage = "Gender is required.")]
            public string Gender { get; set; }

            public int Year { get; set; }

            public int Month { get; set; }

            public int Day { get; set; }

            public bool Agree { get; set; }
        }
    }
}