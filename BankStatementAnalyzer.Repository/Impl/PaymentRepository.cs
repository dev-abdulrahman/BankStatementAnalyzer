using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class PaymentRepository : GenericRepository<BankStatementAnalyzerContext, Payment>, IPaymentRepository
    {
        public PaymentRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}