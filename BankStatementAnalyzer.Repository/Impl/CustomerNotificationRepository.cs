using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CustomerNotificationRepository : GenericRepository<BankStatementAnalyzerContext, CustomerNotification>, ICustomerNotificationRepository
    {
        public CustomerNotificationRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
