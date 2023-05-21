using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class BannersRepository : GenericRepository<BankStatementAnalyzerContext, Banners>, IBannersRepository
    {
        public BannersRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
