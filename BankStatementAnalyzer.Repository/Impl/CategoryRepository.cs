using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CategoryRepository : GenericRepository<BankStatementAnalyzerContext, Category>, ICategoryRepository
    {
        public CategoryRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}