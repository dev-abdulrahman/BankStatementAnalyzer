using System;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class CustomerRefferalCodeMapping
    {
        public CustomerRefferalCodeMapping()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string SelfCode { get; set; }

        public string RefferalCode { get; set; }

        public bool IsCodeUsed { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}