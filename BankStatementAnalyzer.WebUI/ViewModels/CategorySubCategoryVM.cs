using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class CategorySubCategoryVM
    {
        public Category Category { get; set; }

        public SubCategory SubCategory { get; set; }
        public List<Product> products { get; set; }
    }
}