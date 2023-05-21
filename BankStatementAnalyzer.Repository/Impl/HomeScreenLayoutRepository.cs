using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class HomeScreenLayoutRepository : GenericRepository<BankStatementAnalyzerContext, HomeScreenLayout>, IHomeScreenLayoutRepository
    {
        public HomeScreenLayoutRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
