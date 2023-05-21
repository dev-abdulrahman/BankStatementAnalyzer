using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class CarouselViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Number of Images")]
        public int ImageCount { get; set; }

        [Required]
        public string SelectedClassified { get; set; }

        [Display(Name = "Select Classified")]
        public IEnumerable<SelectListItem> Classified { get; set; }

        [Display(Name = "Upload Images")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public HttpPostedFileBase[] Images { get; set; }
    }
}