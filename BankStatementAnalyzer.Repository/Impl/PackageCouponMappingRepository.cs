using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class PackageCouponMappingRepository : GenericRepository<BankStatementAnalyzerContext, PackageCouponMapping>, IPackageCouponMappingRepository
    {
        public PackageCouponMappingRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}