using System;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class TimeSlot : BaseModel
    {
        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}