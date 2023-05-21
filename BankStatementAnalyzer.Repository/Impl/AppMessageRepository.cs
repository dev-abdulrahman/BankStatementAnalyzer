using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class AppMessageRepository : GenericRepository<BankStatementAnalyzerContext, AppMessage>, IAppMessageRepository
    {
        public AppMessageRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}