using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public class Order : BaseModel
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            Payments = new List<Payment>();
        }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public decimal Total { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal BalanceAmount { get; set; }

        public decimal AdvanceAmount { get; set; }

        public string PaymentStatus { get; set; }

        [StringLength(200)]
        public string TransactionID { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public virtual List<Payment> Payments { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string TransactionReference { get; set; }

        [ForeignKey("Address")]
        public int? AddressID { get; set; }
        public virtual Address Address { get; set; }

        [ForeignKey("OrderType")]
        public int OrderTypeId { get; set; }
        public virtual OrderType OrderType { get; set; }

        [ForeignKey("Package")]
        public int? PackageId { get; set; }
        public virtual Package Package { get; set; }

        public bool IsUrgent { get; set; }

        public PaymentType PaymentType { get; set; }

        [StringLength(10)]
        [Display(Name = "Order Id")]
        public string SystemOrderID { get; set; }

        [StringLength(1024)]
        public string Remark { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [ForeignKey("Coupon")]
        public int? CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }

        public double Tip { get; set; }

        public string MerchantCode { get; set; }

        public double PaymentToVendor { get; set; }

        [NotMapped]
        public string CustomerName { get; set; }
    }

    public enum PaymentType
    {
        COD,
        NetBanking,
        CardPayment
    }
}