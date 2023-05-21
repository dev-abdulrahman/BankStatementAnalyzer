using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ClothsRepository : GenericRepository<BankStatementAnalyzerContext, Cloths>, IClothsRepository
    {
        public ClothsRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}