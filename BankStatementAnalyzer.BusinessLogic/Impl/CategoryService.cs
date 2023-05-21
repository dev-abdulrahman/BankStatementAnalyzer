using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void Add(Category entity)
        {
            categoryRepository.Add(entity);
        }

        public void Delete(Category entity)
        {
            categoryRepository.Delete(entity);
        }

        public void Edit(Category entity)
        {
            categoryRepository.Edit(entity);
        }

        public IQueryable<Category> FindBy(Expression<Func<Category, bool>> predicate)
        {
            return categoryRepository.FindBy(predicate);
        }

        public IQueryable<Category> GetAll()
        {
            return categoryRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            categoryRepository.Save();
        }
    }
}