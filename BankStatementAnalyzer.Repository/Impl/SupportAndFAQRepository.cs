using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class SupportAndFAQRepository : GenericRepository<BankStatementAnalyzerContext, SupportAndFAQ>, ISupportAndFAQRepository
    {
        public SupportAndFAQRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}