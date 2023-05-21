using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.Permission
{
    public class PermissionVModel
    {
        public PermissionVModel()
        {
            PermissionInfo = new BankStatementAnalyzer.Models.Permission();
            this.Actions = new List<ActionVM>();
        }

        public BankStatementAnalyzer.Models.Permission PermissionInfo { get; set; }

        public List<ActionVM> Actions { get; set; }

        public bool IsSelected { get; set; }

        public static List<PermissionVModel> ConvertAll(List<BankStatementAnalyzer.Models.Permission> permissions)
        {
            return permissions.ConvertAll(r => new PermissionVModel { PermissionInfo = r });
        }
    }
}