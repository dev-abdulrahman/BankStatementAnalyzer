using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.Models
{
    public class ClassifiedContacts : BaseModel
    {
        public int ClassifiedId { get; set; }

        [Phone]
        [StringLength(15)]
        public string ContactNo { get; set; }
    }
}
