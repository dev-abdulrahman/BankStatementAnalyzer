using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedKeywordsService : IClassifiedKeywordsService
    {
        private readonly IClassifiedKeywordsRepository classifiedKeywordsRepository; 

        public ClassifiedKeywordsService(IClassifiedKeywordsRepository classifiedKeywordsRepository)
        {
            this.classifiedKeywordsRepository = classifiedKeywordsRepository;
        }

        public void Add(ClassifiedKeywords entity)
        {
            classifiedKeywordsRepository.Add(entity);
        }

        public void Delete(ClassifiedKeywords entity)
        {
            classifiedKeywordsRepository.Delete(entity);
        }

        public void Edit(ClassifiedKeywords entity)
        {
            classifiedKeywordsRepository.Edit(entity);
        }

        public IQueryable<ClassifiedKeywords> FindBy(Expression<Func<ClassifiedKeywords, bool>> predicate)
        {
            return classifiedKeywordsRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedKeywords> GetAll()
        {
            return classifiedKeywordsRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            classifiedKeywordsRepository.Save();
        }
    }
}
