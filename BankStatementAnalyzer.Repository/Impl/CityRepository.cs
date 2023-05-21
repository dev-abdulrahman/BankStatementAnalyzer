using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CityRepository : GenericRepository<BankStatementAnalyzerContext, City>, ICityRepository
    {
        public CityRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}