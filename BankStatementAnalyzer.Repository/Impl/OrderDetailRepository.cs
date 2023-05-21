using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class OrderDetailRepository : GenericRepository<BankStatementAnalyzerContext, OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(BankStatementAnalyzerContext context) : base(context)
        {

        }
    }
}