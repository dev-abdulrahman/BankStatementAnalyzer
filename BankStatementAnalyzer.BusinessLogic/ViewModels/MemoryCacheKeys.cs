using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.ViewModels
{
    public class MemoryCacheKeys
    {
        public const string KEY_PERMISSIONS = "permissions_";
        public const string KEY_MENUS = "menus_";
        public const string KEY_USER = "user_";
        public const string KEY_SYSTEMSETTING = "systemsetting_";

        public static List<string> GetAllForUser(string username)
        {
            return new List<string>()
            {
                KEY_PERMISSIONS + username,
                KEY_MENUS + username,
                KEY_USER + username
            };
        }
    }
}
