using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IActionService: IGenericService<Action>
    {
        bool IsExist(string name);
    }
}
