using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.Roles
{
    public class PermissionVM
    {
        public PermissionVM()
        {
            PermissionInfo = new BankStatementAnalyzer.Models.Permission();
        }

        public BankStatementAnalyzer.Models.Permission PermissionInfo { get; set; }

        public bool IsSelected { get; set; }

        public static List<PermissionVM> ConvertAll(List<BankStatementAnalyzer.Models.Permission> permissions)
        {
            return permissions.ConvertAll(p => new PermissionVM { PermissionInfo = p });
        }
    }
}