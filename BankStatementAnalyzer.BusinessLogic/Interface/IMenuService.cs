using BankStatementAnalyzer.BusinessLogic.Common;
using System.Collections.Generic;
using System.Linq;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IMenuService : IGenericService<Models.Menu>
    {
        IQueryable<Models.Menu> GetAllFirstLevel();

        List<Models.Menu> GetAllForUser(string username, bool isSystemAdmin);
    }
}