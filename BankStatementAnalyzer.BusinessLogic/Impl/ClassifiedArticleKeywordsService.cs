using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedArticleKeywordsService : IClassifiedArticleKeywordsService
    {
        private readonly IClassifiedArticleKeywordsRepository classifiedArticleKeywordsRepository;

        public ClassifiedArticleKeywordsService(IClassifiedArticleKeywordsRepository classifiedArticleKeywordsRepository)
        {
            this.classifiedArticleKeywordsRepository = classifiedArticleKeywordsRepository;
        }

        public void Add(ClassifiedArticleKeywords entity)
        {
            classifiedArticleKeywordsRepository.Add(entity);
        }

        public void Delete(ClassifiedArticleKeywords entity)
        {
            classifiedArticleKeywordsRepository.Delete(entity);
        }

        public void Edit(ClassifiedArticleKeywords entity)
        {
            classifiedArticleKeywordsRepository.Edit(entity);
        }

        public IQueryable<ClassifiedArticleKeywords> FindBy(Expression<Func<ClassifiedArticleKeywords, bool>> predicate)
        {
            return classifiedArticleKeywordsRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedArticleKeywords> GetAll()
        {
            return classifiedArticleKeywordsRepository.GetAll();
        }

        public void Save()
        {
            classifiedArticleKeywordsRepository.Save();
        }
    }
}
