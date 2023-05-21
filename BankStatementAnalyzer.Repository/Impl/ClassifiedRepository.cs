using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ClassifiedRepository : GenericRepository<BankStatementAnalyzerContext, Classified>, IClassifiedRepository
    {
        public ClassifiedRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
