using System;

namespace BankStatementAnalyzer.BusinessLogic.Common
{
    public static class Helpers
    {
        public static string GetTimeFromAndTo(this DateTime from, DateTime to)
        {
            var fm = from.AddHours(24).ToString("hh:mm tt");
            var two = to.AddHours(24).ToString("hh:mm tt");

            return $"{fm}-{two}";
        }
    }
}
