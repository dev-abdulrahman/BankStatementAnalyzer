using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ClassifiedArticleCategoryRepository : GenericRepository<BankStatementAnalyzerContext, ClassifiedArticleCategory>, IClassifiedArticleCategoryRepository
    {
        public ClassifiedArticleCategoryRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
