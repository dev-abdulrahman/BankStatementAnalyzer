using BankStatementAnalyzer.Models;
using System;

namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class Customer
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string PhoneNumber { get; set; }

        public string UID { get; set; }

        public string NickName { get; set; }

        public string HouseNo { get; set; }

        public string Pincode { get; set; }

        public string Area { get; set; }

        public string CompanyKey { get; set; }

        public int CompanyId { get; set; }

        public string Street { get; set; }

        public string Email { get; set; }

        public string DeviceId { get; set; }

        public string FriendCode { get; set; }

        public string ReferralCode { get; set; }

        public Gender Gender { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public DateTime BirthDate { get; set; }

        public string Image { get; set; }

    }
}