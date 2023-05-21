using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.Models
{
    public class ClassifiedRating : BaseModel
    {
        public string Name { get; set; }

        public string Review { get; set; }

        public int ClassifiedId { get; set; }

        public int CustomerId { get; set; }

        public int CategoryId { get; set; }

        public double Rating { get; set; }
    }
}
