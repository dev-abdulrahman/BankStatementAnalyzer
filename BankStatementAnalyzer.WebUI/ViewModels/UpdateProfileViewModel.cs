using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class UpdateProfileViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}