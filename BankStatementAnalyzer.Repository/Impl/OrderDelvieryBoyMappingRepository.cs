using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class OrderDelvieryBoyMappingRepository : GenericRepository<BankStatementAnalyzerContext, OrderDelvieryBoyMapping>, IOrderDelvieryBoyMappingRepository
    {
        public OrderDelvieryBoyMappingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}