using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ClassifiedArticleImagesRepository : GenericRepository<BankStatementAnalyzerContext, ClassifiedArticleImages>, IClassifiedArticleImagesRepository
    {
        public ClassifiedArticleImagesRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
