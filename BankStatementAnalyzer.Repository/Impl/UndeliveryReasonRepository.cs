using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class UndeliveryReasonRepository : GenericRepository<BankStatementAnalyzerContext, UndeliveryReason>, IUndeliveryReasonRepository
    {
        public UndeliveryReasonRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}