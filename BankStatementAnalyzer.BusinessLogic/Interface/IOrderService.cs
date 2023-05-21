using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IOrderService : ISearchService<Order>, IGenericService<Order>
    {
    }
}