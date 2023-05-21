using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedArticleCategoryService : IClassifiedArticleCategoryService
    {
        private readonly IClassifiedArticleCategoryRepository classifiedArticleCategoryRepository;

        public ClassifiedArticleCategoryService(IClassifiedArticleCategoryRepository classifiedArticleCategoryRepository)
        {
            this.classifiedArticleCategoryRepository = classifiedArticleCategoryRepository;
        }

        public void Add(ClassifiedArticleCategory entity)
        {
            classifiedArticleCategoryRepository.Add(entity);
        }

        public void Delete(ClassifiedArticleCategory entity)
        {
            classifiedArticleCategoryRepository.Delete(entity);
        }

        public void Edit(ClassifiedArticleCategory entity)
        {
            classifiedArticleCategoryRepository.Edit(entity);
        }

        public IQueryable<ClassifiedArticleCategory> FindBy(Expression<Func<ClassifiedArticleCategory, bool>> predicate)
        {
            return classifiedArticleCategoryRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedArticleCategory> GetAll()
        {
            return classifiedArticleCategoryRepository.GetAll();
        }

        public void Save()
        {
            classifiedArticleCategoryRepository.Save();
        }
    }
}
