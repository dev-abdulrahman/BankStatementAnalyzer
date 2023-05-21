using BankStatementAnalyzer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
            UserInfo = new User();
            this.Roles = new List<Roles.RoleVM>();
            Companies = new List<string>();
        }

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
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<string> Companies { get; set; }

        public User UserInfo { get; set; }
        public List<Roles.RoleVM> Roles { get; set; }
    }
}