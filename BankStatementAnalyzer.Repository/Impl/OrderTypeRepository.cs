using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class OrderTypeRepository : GenericRepository<BankStatementAnalyzerContext, OrderType>, IOrderTypeRepository
    {
        public OrderTypeRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}