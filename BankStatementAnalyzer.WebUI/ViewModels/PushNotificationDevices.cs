using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public enum UserType
    {
        [Display(Name = "Select User")]
        SelectUser,

        [Display(Name = "All User")]
        AllUser
    }

    public class PushNotificationDevices
    {
        [Required]
        public List<string> DeviceIds { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public UserType UserType { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }
}