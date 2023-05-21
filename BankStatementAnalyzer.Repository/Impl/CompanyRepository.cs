using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CompanyRepository : GenericRepository<BankStatementAnalyzerContext, Company>, ICompanyRepository
    {
        public CompanyRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}