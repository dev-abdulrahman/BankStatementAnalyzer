using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class MenuRepository : GenericRepository<BankStatementAnalyzerContext, Menu>, IMenuRepository
    {
        public MenuRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}