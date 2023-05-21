using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.Roles
{
    public class RoleVM
    {
        public RoleVM()
        {
            RoleInfo = new Role();
            this.Permissions = new List<PermissionVM>();
        }

        public Role RoleInfo { get; set; }

        public List<PermissionVM> Permissions { get; set; }

        public bool IsSelected { get; set; }

        public static List<RoleVM> ConvertAll(List<Role> roles)
        {
            return roles.ConvertAll(r => new RoleVM { RoleInfo = r });
        }
    }
}