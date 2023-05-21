using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ClassifiedArticleKeywordsRepository : GenericRepository<BankStatementAnalyzerContext, ClassifiedArticleKeywords>, IClassifiedArticleKeywordsRepository
    {
        public ClassifiedArticleKeywordsRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
