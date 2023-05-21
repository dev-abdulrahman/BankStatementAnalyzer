using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class BestDealRepository : GenericRepository<BankStatementAnalyzerContext, BestDeal>, IBestDealRepository
    {
        public BestDealRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
