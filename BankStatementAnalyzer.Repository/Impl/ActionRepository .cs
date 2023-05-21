using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ActionRepository : GenericRepository<BankStatementAnalyzerContext, Action>, IActionRepository
    {
        public ActionRepository(BankStatementAnalyzerContext ecommerceContext) : base(ecommerceContext)
        {
        }
    }
}