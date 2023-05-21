using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class OrderStatusMappingRepository : GenericRepository<BankStatementAnalyzerContext, OrderStatusMapping>, IOrderStatusMappingRepository
    {
        public OrderStatusMappingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}