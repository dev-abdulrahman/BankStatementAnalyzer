using System;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class PackageCouponMapping
    {
        [Key]
        public int Id { get; set; }

        public int CouponId { get; set; }

        public int PackageId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}