using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CouponRepository : GenericRepository<BankStatementAnalyzerContext, Coupon>, ICouponRepository
    {
        public CouponRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}