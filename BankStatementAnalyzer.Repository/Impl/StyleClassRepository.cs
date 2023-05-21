using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class StyleClassRepository : GenericRepository<BankStatementAnalyzerContext, StyleClass>, IStyleClassRepository
    {
        public StyleClassRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}