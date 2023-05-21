using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class DeliveryBoyRepository : GenericRepository<BankStatementAnalyzerContext, DeliveryBoy>, IDeliveryBoyRepository
    {
        public DeliveryBoyRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}