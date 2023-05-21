using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CustomerCreditMappingRepository : GenericRepository<BankStatementAnalyzerContext, CustomerCreditMapping>, ICustomerCreditMappingRepository
    {
        public CustomerCreditMappingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}