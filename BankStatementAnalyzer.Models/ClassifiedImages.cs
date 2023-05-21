using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.Models
{
    public class ClassifiedImages : BaseModel
    {
        public int ClassifiedId { get; set; }

        public string ImageUrl { get; set; }
    }
}
