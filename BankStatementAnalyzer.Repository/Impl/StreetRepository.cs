using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class StreetRepository : GenericRepository<BankStatementAnalyzerContext, Street>, IStreetRepository
    {
        public StreetRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}