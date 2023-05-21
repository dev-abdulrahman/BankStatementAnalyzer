using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class StyleTraitValueProductRepository : GenericRepository<BankStatementAnalyzerContext, StyleTraitValueProduct>, IStyleTraitValueProductRepository
    {
        public StyleTraitValueProductRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}