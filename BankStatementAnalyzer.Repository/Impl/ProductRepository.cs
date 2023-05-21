using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class ProductRepository : GenericRepository<BankStatementAnalyzerContext, Product>, IProductRepository
    {
        public ProductRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}