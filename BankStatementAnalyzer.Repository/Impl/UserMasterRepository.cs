using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class UserMasterRepository :
        GenericRepository<BankStatementAnalyzerContext, User>, IUserMasterRepository
    {
        public UserMasterRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}