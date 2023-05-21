using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class OrderVendorMappingRepository : GenericRepository<BankStatementAnalyzerContext, OrderVendorMapping>, IOrderVendorMappingRepository
    {
        public OrderVendorMappingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}