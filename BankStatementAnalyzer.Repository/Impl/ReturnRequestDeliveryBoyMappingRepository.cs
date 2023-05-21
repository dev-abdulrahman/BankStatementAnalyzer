using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ReturnRequestDeliveryBoyMappingRepository : GenericRepository<BankStatementAnalyzerContext, ReturnRequestDeliveryBoyMapping>, IReturnRequestDeliveryBoyMappingRepository
    {
        public ReturnRequestDeliveryBoyMappingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}