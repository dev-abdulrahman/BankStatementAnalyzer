using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;
using System.Web;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IClassifiedService : IGenericService<Classified>
    {
        bool ValidateImageExtension(HttpPostedFileBase[] model);
    }
}
