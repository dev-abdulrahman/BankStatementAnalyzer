using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface ISubCategoryService : IGenericService<SubCategory>
    {
        IEnumerable<SubCategory> GetSubcategoryByCategory(int categoryMasterId);

        bool IsExist(string subcategoryName, int categoryId);
    }
}