using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class PageManagerRepository : GenericRepository<BankStatementAnalyzerContext, PageManager>, IPageManagerRepository
    {
        public PageManagerRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}