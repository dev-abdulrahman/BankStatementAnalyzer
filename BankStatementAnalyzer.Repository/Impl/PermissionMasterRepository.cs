using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class PermissionMasterRepository :
        GenericRepository<BankStatementAnalyzerContext, Permission>, IPermissionMasterRepository
    {
        public PermissionMasterRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}