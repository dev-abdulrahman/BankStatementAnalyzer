using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository subCategoryRepository;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public void Add(SubCategory entity)
        {
            subCategoryRepository.Add(entity);
        }

        public void Delete(SubCategory entity)
        {
            subCategoryRepository.Delete(entity);
        }

        public void Edit(SubCategory entity)
        {
            subCategoryRepository.Edit(entity);
        }

        public IQueryable<SubCategory> FindBy(Expression<Func<SubCategory, bool>> predicate)
        {
            return subCategoryRepository.FindBy(predicate);
        }

        public IQueryable<SubCategory> GetAll()
        {
            return subCategoryRepository.GetAll();
        }

        public IEnumerable<SubCategory> GetSubcategoryByCategory(int categoryMasterId)
        {
            return this.GetAll().Where(x => x.CategoryID == categoryMasterId);
        }

        public bool IsExist(string subcategoryName, int categoryId)
        {
            return subCategoryRepository.FindBy(x => x.Name == subcategoryName && x.CategoryID == categoryId).Any();
        }

        public void Save()
        {
            subCategoryRepository.Save();
        }
    }
}