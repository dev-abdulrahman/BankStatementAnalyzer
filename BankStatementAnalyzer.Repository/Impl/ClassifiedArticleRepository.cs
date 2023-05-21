using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ClassifiedArticleRepository : GenericRepository<BankStatementAnalyzerContext, ClassifiedArticle>, IClassifiedArticleRepository
    {
        public ClassifiedArticleRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
