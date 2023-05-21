using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CustomerRepository : GenericRepository<BankStatementAnalyzerContext, Customer>, ICustomerRepository
    {
        public CustomerRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}