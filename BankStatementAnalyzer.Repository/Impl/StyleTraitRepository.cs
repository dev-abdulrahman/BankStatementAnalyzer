using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class StyleTraitRepository : GenericRepository<BankStatementAnalyzerContext, StyleTrait>, IStyleTraitRepository
    {
        public StyleTraitRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}