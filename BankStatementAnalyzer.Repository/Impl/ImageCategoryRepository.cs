using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ImageCategoryRepository : GenericRepository<BankStatementAnalyzerContext, ImageCategory>, IImageCategoryRepository
    {
        public ImageCategoryRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}