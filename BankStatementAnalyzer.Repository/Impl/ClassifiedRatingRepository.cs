using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ClassifiedRatingRepository : GenericRepository<BankStatementAnalyzerContext, ClassifiedRating>, IClassifiedRatingRepository
    {
        public ClassifiedRatingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
