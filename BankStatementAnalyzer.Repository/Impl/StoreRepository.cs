using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class StoreRepository : GenericRepository<BankStatementAnalyzerContext, Store>, IStoreRepository
    {
        public StoreRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}