using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class ClassifiedIndexViewModel
    {

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "City")]
        public string Location { get; set; }

        public string Description { get; set; }

        [Display(Name = "Special In")]
        public string SpecialistIn { get; set; }

        public List<int> SelectedCategory { get; set; }

        public List<int> SelectedSubCategory { get; set; }

        public string SelectedCategories { get; set; }

        [StringLength(1024)]
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [StringLength(1024)]
        public string Address2 { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public AvailabilityType Availability { get; set; }

        public double Rating { get; set; }

        [Required(ErrorMessage = "Please enter Mobile")]
        [Display(Name = "Mobile")]
        [Phone]
        [StringLength(15)]
        public string ContactNo { get; set; }

        [Display(Name = "Mobile (Optional)")]
        [Phone]
        [StringLength(15)]
        public string OptionalContactNo { get; set; }

        [Display(Name = "Upload Images")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public HttpPostedFileBase[] Images { get; set; }

        public string Keywords { get; set; }

        public bool IsEditMode { get; set; } = false;

    }
}