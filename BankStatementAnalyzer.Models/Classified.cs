using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public enum AvailabilityType
    {
        OPEN,
        CLOSE
    }

    public class Classified : BaseModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string SpecialistIn { get; set; }

        [StringLength(1024)]
        public string Address1 { get; set; }

        [StringLength(1024)]
        public string Address2 { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public AvailabilityType Availability { get; set; }
    }
}
