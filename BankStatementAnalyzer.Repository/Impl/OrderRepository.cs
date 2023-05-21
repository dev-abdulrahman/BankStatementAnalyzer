using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class OrderRepository : GenericRepository<BankStatementAnalyzerContext, Order>, IOrderRepository
    {
        public OrderRepository(BankStatementAnalyzerContext context) : base(context)
        {

        }
    }
}