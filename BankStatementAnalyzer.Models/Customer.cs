using System;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public enum Gender { Male, Female, Other}

    public enum MaritalStatus { Single, Married, Other }

    public class Customer
    {
        public Customer()
        {
            CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UID { get; set; }

        [StringLength(100)]
        public string NickName { get; set; }

        [StringLength(50)]
        public string HouseNo { get; set; }

        [StringLength(6)]
        public string Pincode { get; set; }

        [StringLength(50)]
        public string Area { get; set; }

        public int CompanyId { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public string DeviceId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ReferralCode { get; set; }

        public string ReferFromCode { get; set; }

        public bool IsCreditApplicable { get; set; }

        public string CreateBy { get; set; }

        public Gender Gender { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public DateTime BirthDate { get; set; }

        public string Image { get; set; }

    }
}