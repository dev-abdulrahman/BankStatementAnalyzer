using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public  interface ISystemSettingService : IGenericService<SystemSetting>
    {
        bool IsExist(string name);
        bool IsDuplicate(string name,string main);

    }
}