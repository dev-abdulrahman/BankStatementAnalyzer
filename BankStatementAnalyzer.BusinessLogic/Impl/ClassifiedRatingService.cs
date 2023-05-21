using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedRatingService : IClassifiedRatingService
    {
        private readonly IClassifiedRatingRepository classifiedRatingRepository;

        public ClassifiedRatingService(IClassifiedRatingRepository classifiedRatingRepository)
        {
            this.classifiedRatingRepository = classifiedRatingRepository;
        }

        public void Add(ClassifiedRating entity)
        {
            classifiedRatingRepository.Add(entity);
        }

        public void Delete(ClassifiedRating entity)
        {
            classifiedRatingRepository.Delete(entity);
        }

        public void Edit(ClassifiedRating entity)
        {
            classifiedRatingRepository.Edit(entity);
        }

        public IQueryable<ClassifiedRating> FindBy(Expression<Func<ClassifiedRating, bool>> predicate)
        {
            return classifiedRatingRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedRating> GetAll()
        {
            return classifiedRatingRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            classifiedRatingRepository.Save();
        }
    }
}
