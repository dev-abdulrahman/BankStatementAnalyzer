using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public void Add(City entity)
        {
            cityRepository.Add(entity);
        }

        public void Delete(City entity)
        {
            cityRepository.Delete(entity);
        }

        public void Edit(City entity)
        {
            cityRepository.Edit(entity);
        }

        public IQueryable<City> FindBy(Expression<Func<City, bool>> predicate)
        {
            return cityRepository.FindBy(predicate);
        }

        public IQueryable<City> GetAll()
        {
            return cityRepository.GetAll();
        }

        public void Save()
        {
            cityRepository.Save();
        }
    }
}