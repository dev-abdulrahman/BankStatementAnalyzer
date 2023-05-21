using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class WalletRepository : GenericRepository<BankStatementAnalyzerContext, Wallet>, IWalletRepository
    {
        public WalletRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}