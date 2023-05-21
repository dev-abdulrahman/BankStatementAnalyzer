using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IUserMasterService : IGenericService<User>
    {
        User GetUserByUsername(string username);

        Role GetRoleById(int id);
    }
}