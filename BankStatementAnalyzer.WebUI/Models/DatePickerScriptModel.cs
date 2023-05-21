using System;

namespace BankStatementAnalyzer.WebUI.Models
{
    public class DatePickerScriptModel
    {
        public string TextBoxId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}