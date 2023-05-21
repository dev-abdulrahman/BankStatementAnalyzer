using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class AddressRepository : GenericRepository<BankStatementAnalyzerContext, Address>, IAddressRepository
    {
        public AddressRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}