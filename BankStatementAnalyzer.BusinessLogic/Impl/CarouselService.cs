using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CarouselService : ICarouselService
    {
        private readonly ICarouselRepository carouselRepository; 

        public CarouselService(ICarouselRepository carouselRepository)
        {
            this.carouselRepository = carouselRepository;
        }

        public void Add(Carousel entity)
        {
            carouselRepository.Add(entity);
        }

        public void Delete(Carousel entity)
        {
            carouselRepository.Delete(entity);
        }

        public void Edit(Carousel entity)
        {
            carouselRepository.Edit(entity);
        }

        public IQueryable<Carousel> FindBy(Expression<Func<Carousel, bool>> predicate)
        {
            return carouselRepository.FindBy(predicate);
        }

        public IQueryable<Carousel> GetAll()
        {
            return carouselRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            carouselRepository.Save();
        }
    }
}
