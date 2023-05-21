using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedArticleImagesService : IClassifiedArticleImagesService
    {
        private readonly IClassifiedArticleImagesRepository classifiedArticleImagesRepository;

        public ClassifiedArticleImagesService(IClassifiedArticleImagesRepository classifiedArticleImagesRepository)
        {
            this.classifiedArticleImagesRepository = classifiedArticleImagesRepository;
        }

        public void Add(ClassifiedArticleImages entity)
        {
            classifiedArticleImagesRepository.Add(entity);
        }

        public void Delete(ClassifiedArticleImages entity)
        {
            classifiedArticleImagesRepository.Delete(entity);
        }

        public void Edit(ClassifiedArticleImages entity)
        {
            classifiedArticleImagesRepository.Edit(entity);
        }

        public IQueryable<ClassifiedArticleImages> FindBy(Expression<Func<ClassifiedArticleImages, bool>> predicate)
        {
            return classifiedArticleImagesRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedArticleImages> GetAll()
        {
            return classifiedArticleImagesRepository.GetAll();
        }

        public void Save()
        {
            classifiedArticleImagesRepository.Save();
        }
    }
}
