using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class GalleryRepository : GenericRepository<BankStatementAnalyzerContext, Gallery>, IGalleryRepository
    {
        public GalleryRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}