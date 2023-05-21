using System;
using System.Linq;
using System.Linq.Expressions;
using BankStatementAnalyzer.Repository.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.BusinessLogic.Interface;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class HomeScreenLayoutService : IHomeScreenLayoutService
    {
        private readonly IHomeScreenLayoutRepository homeScreenLayoutRepository;

        public HomeScreenLayoutService(IHomeScreenLayoutRepository homeScreenLayoutRepository)
        {
            this.homeScreenLayoutRepository = homeScreenLayoutRepository;
        }

        public void Add(HomeScreenLayout entity)
        {
            homeScreenLayoutRepository.Add(entity);
        }

        public void Delete(HomeScreenLayout entity)
        {
            homeScreenLayoutRepository.Delete(entity);
        }

        public void Edit(HomeScreenLayout entity)
        {
            homeScreenLayoutRepository.Edit(entity);
        }

        public IQueryable<HomeScreenLayout> FindBy(Expression<Func<HomeScreenLayout, bool>> predicate)
        {
            return homeScreenLayoutRepository.FindBy(predicate);
        }

        public IQueryable<HomeScreenLayout> GetAll()
        {
            return homeScreenLayoutRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            homeScreenLayoutRepository.Save();
        }
    }
}
