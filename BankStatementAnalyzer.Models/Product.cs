using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BankStatementAnalyzer.Models
{
    public enum DiscountType { Fixed, Percent }

    public class Product : BaseModel
    {
        public Product()
        {
            ListStyleVariantValue = new List<string>();
            Files = new List<File>();
        }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Product Title")]
        public string Title { get; set; }

        [Display(Name = "Shirt Title")]
        [StringLength(150)]
        public string ShortTitle { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }

        public int Liters { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ActivateOn { get; set; }

        public DateTime? DeactivateOn { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public DiscountType DiscountType { get; set; }

        public decimal SpecialPrice { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsReturnAble { get; set; }

        [ForeignKey("SubCategory")]
        public int? SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public virtual List<File> Files { get; set; }

        [ForeignKey("UnitOfMeasure")]
        public int UnitOfMeasureId { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }

        [NotMapped]
        public string CurrentActiveTab { get; set; }

        [NotMapped]
        public List<string> ListStyleVariantValue { get; set; }

        [NotMapped]
        public int DdStyleClass { get; set; }

        [NotMapped]
        public int DdStyleTrait { get; set; }

        [NotMapped]
        public int CategoryId { get; set; }

        [NotMapped]
        public int DdSubCategory { get; set; }
    }
}