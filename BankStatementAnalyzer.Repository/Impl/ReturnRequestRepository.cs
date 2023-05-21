using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ReturnRequestRepository : GenericRepository<BankStatementAnalyzerContext, ReturnRequest>, IReturnRequestRepository
    {
        public ReturnRequestRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}