using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class HomeScreenViewModel
    {
        [Display(Name = "Icon")]
        public IEnumerable<SelectListItem> Icons { get; set; }

        public string SelectedIcon { get; set; }

        [Display(Name = "Color")]
        public IEnumerable<SelectListItem> Color { get; set; }

        public int SelectedColor { get; set; }

        [Display(Name = "Cateogry")]
        public IEnumerable<SelectListItem> Category { get; set; }

        public int SelectedCategory { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}