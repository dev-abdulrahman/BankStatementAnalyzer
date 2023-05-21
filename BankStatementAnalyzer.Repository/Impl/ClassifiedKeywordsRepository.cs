using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ClassifiedKeywordsRepository : GenericRepository<BankStatementAnalyzerContext, ClassifiedKeywords>, IClassifiedKeywordsRepository
    {
        public ClassifiedKeywordsRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
