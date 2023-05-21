using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedCategoryService : IClassifiedCategoryService
    {
        private readonly IClassifiedCategoryRepository classifiedCategoryRepository;

        public ClassifiedCategoryService(IClassifiedCategoryRepository classifiedCategoryRepository)
        {
            this.classifiedCategoryRepository = classifiedCategoryRepository;
        }

        public void Add(ClassifiedCategory entity)
        {
            classifiedCategoryRepository.Add(entity);
        }

        public void Delete(ClassifiedCategory entity)
        {
            classifiedCategoryRepository.Delete(entity);
        }

        public void Edit(ClassifiedCategory entity)
        {
            classifiedCategoryRepository.Edit(entity);
        }

        public IQueryable<ClassifiedCategory> FindBy(Expression<Func<ClassifiedCategory, bool>> predicate)
        {
            return classifiedCategoryRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedCategory> GetAll()
        {
            return classifiedCategoryRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            classifiedCategoryRepository.Save();
        }
    }
}
