using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace BankStatementAnalyzer.WebUI
{
    public static class ClaimExtensions
    {
        public static string Role(this IIdentity value)
        {
            if (!(value is ClaimsIdentity))
                return null;

            return (value as ClaimsIdentity).Claims.Where(c => c.Type == ClaimTypes.Role)?.FirstOrDefault()?.Value;
        }

        public static string Username(this IIdentity value)
        {
            if (!(value is ClaimsIdentity))
                return null;

            return (value as ClaimsIdentity).Name;
        }

        public static string FullName(this IIdentity value)
        {
            if (!(value is ClaimsIdentity))
                return null;

            return (value as ClaimsIdentity).Claims.Where(c => c.Type == "FullName")?.FirstOrDefault()?.Value;
        }
    }
}