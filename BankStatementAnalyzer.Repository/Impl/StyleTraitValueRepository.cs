using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class StyleTraitValueRepository : GenericRepository<BankStatementAnalyzerContext, StyleTraitValue>, IStyleTraitValueRepository
    {
        public StyleTraitValueRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}