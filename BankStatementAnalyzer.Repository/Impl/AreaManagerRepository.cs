using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class AreaManagerRepository : GenericRepository<BankStatementAnalyzerContext, AreaManager>, IAreaManagerRepository
    {
        public AreaManagerRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}