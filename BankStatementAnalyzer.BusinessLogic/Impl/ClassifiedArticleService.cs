using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedArticleService : IClassifiedArticleService
    {
        private readonly IClassifiedArticleRepository classifiedArticleRepository;

        public ClassifiedArticleService(IClassifiedArticleRepository classifiedArticleRepository)
        {
            this.classifiedArticleRepository = classifiedArticleRepository;
        }

        public void Add(ClassifiedArticle entity)
        {
            classifiedArticleRepository.Add(entity);
        }

        public void Delete(ClassifiedArticle entity)
        {
            classifiedArticleRepository.Delete(entity);
        }

        public void Edit(ClassifiedArticle entity)
        {
            classifiedArticleRepository.Edit(entity);
        }

        public IQueryable<ClassifiedArticle> FindBy(Expression<Func<ClassifiedArticle, bool>> predicate)
        {
            return classifiedArticleRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedArticle> GetAll()
        {
            return classifiedArticleRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            classifiedArticleRepository.Save();
        }
    }
}
