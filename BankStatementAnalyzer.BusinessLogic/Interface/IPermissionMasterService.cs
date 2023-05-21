using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IPermissionMasterService : IGenericService<Permission>
    {
        List<string> GetAllForUser(string username);
    }
}