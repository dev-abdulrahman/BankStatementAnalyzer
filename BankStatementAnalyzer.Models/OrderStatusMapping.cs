using System;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class OrderStatusMapping
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? EstimatedDate { get; set; }
    }
}