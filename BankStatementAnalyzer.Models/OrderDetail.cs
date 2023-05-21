using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public enum OrderStatus
    {
        Ordered,

        [Display(Name = "Assigned For PickUp")]
        AssignedForPickUp,

        PickedUp,

        [Display(Name = "Under Processing")]
        UnderProcessing,

        [Display(Name = "Ready For Delivery")]
        ReadyForDelivery,

        [Display(Name = "Assigned For Delivery")]
        AssignedForDelivery,

        Delivered,

        Removed,

        Cancelled
    }

    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public Order Order { get; set; }

        [ForeignKey("ServiceType")]
        public int ServiceTypeID { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        [ForeignKey("Cloths")]
        public int ClothsID { get; set; }
        public virtual Cloths Cloths { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPercentage { get; set; }

        [StringLength(15)]
        public string ContactNo { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public bool IsUrgent { get; set; }

        public decimal TotalPayable { get; set; }

        public string Remark { get; set; }
    }
}