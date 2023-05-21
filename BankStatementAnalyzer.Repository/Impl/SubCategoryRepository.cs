using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class SubCategoryRepository : GenericRepository<BankStatementAnalyzerContext, SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}