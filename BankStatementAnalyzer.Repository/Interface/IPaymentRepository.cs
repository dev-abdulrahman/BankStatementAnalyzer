using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;

namespace BankStatementAnalyzer.Repository.Interface
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
    }
}