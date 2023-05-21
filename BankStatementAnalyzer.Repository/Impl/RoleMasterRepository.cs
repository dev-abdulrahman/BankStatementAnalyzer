using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class RoleMasterRepository :
        GenericRepository<BankStatementAnalyzerContext, Role>, IRoleMasterRepository
    {
        public RoleMasterRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}