using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.Permission
{
    public class ActionVM
    {
        public ActionVM()
        {
            ActionInfo = new Action();
        }

        public Action ActionInfo { get; set; }

        public bool IsSelected { get; set; }

        public static List<ActionVM> ConvertAll(List<Action> actions)
        {
            return actions.ConvertAll(p => new ActionVM { ActionInfo = p });
        }
    }
}