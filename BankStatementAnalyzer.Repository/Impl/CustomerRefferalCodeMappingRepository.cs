using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CustomerRefferalCodeMappingRepository : GenericRepository<BankStatementAnalyzerContext, CustomerRefferalCodeMapping>, ICustomerRefferalCodeMappingRepository
    {
        public CustomerRefferalCodeMappingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}