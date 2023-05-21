using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CustomerCreditHistoryRepository : GenericRepository<BankStatementAnalyzerContext, CustomerCreditHistory>, ICustomerCreditHistoryRepository
    {
        public CustomerCreditHistoryRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}