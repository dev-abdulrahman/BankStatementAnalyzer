using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class VendorRepository : GenericRepository<BankStatementAnalyzerContext, Vendor>, IVendorRepository
    {
        public VendorRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}