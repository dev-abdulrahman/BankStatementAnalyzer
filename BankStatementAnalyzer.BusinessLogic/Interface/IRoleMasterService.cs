using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IRoleMasterService : IGenericService<Role>
    {
        Role GetRoleByName(string name, int id);
        Permission GetPermissionById(int id);
    }
}