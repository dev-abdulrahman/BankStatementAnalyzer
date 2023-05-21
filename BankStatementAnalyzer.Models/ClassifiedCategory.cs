using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.Models
{
    public class ClassifiedCategory : BaseModel
    {
        public int ClassifiedId { get; set; }

        public int CategoryId { get; set; }
    }
}
