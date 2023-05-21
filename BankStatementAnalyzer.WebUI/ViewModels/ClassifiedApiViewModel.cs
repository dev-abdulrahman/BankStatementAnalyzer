using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class ClassifiedApiViewModel
    {
        public ClassifiedApiViewModel()
        {
            this.Categories = new List<string>();
            this.Images = new List<string>();
            this.Reviews = new List<ClassifiedRatingVM>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string SpecialistIn { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Availability { get; set; }

        public List<string> Categories { get; set; }

        public double Rating { get; set; }

        public int TotalRatings { get; set; }

        public string ContactNo { get; set; }

        public string OptionalContactNo { get; set; }

        public List<string> Images { get; set; }

        public List<ClassifiedRatingVM> Reviews { get; set; }

        public double Distance { get; set; }

    }
}