using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.Models
{
    public class Carousel : BaseModel
    {
        [Display(Name = "Add Images")]
        public string ImageUrl { get; set; }

        [Display(Name = "Select Classified")]
        public int ClassifiedId { get; set; }
    }
}
