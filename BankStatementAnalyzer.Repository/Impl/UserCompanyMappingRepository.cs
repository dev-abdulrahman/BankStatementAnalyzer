using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class UserCompanyMappingRepository : GenericRepository<BankStatementAnalyzerContext,UserCompanyMapping> , IUserCompanyMappingRepository
    {
        public UserCompanyMappingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}