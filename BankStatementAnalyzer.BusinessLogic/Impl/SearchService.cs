using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.DataTables;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class SearchService<T> : ISearchService<T> where T : class
    {
        protected readonly IGenericRepository<T> genericRepository;

        public SearchService(IGenericRepository<T> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public PagedListResult<T> Search(SearchQuery<T> searchQuery)
        {
            return genericRepository.Search(searchQuery);
        }
    }
}
