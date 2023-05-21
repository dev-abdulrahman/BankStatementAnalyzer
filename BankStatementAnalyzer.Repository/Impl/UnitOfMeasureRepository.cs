using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class UnitOfMeasureRepository : GenericRepository<BankStatementAnalyzerContext, UnitOfMeasure>, IUnitOfMeasureRepository
    {
        public UnitOfMeasureRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}