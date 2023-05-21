using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class PackageRepository : GenericRepository<BankStatementAnalyzerContext, Package>, IPackageRepository
    {
        public PackageRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}