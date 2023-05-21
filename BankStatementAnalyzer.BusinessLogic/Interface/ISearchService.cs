using BankStatementAnalyzer.Repository.DataTables;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface ISearchService<T>  
    {
        PagedListResult<T> Search(SearchQuery<T> searchQuery);
    }
}