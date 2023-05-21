using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class RateCardRepository : GenericRepository<BankStatementAnalyzerContext, RateCard>, IRateCardRepository
    {
        public RateCardRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}