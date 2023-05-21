using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.Common
{
    public class MailModel
    {
        public string FromId { get; set; }

        public IEnumerable<string> ToList { get; set; }

        public IEnumerable<string> CCList { get; set; }

        public string Subject { get; set; }

        public string MessageBody { get; set; }
    }
}
