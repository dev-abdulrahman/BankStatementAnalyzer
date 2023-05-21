using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class BannersService : IBannersService
    {
        private readonly IBannersRepository bannersRepository;

        public BannersService(IBannersRepository bannersRepository)
        {
            this.bannersRepository = bannersRepository;
        }

        public void Add(Banners entity)
        {
            bannersRepository.Add(entity);
        }

        public void Delete(Banners entity)
        {
            bannersRepository.Delete(entity);
        }

        public void Edit(Banners entity)
        {
            bannersRepository.Edit(entity);
        }

        public IQueryable<Banners> FindBy(Expression<Func<Banners, bool>> predicate)
        {
            return bannersRepository.FindBy(predicate);
        }

        public IQueryable<Banners> GetAll()
        {
            return bannersRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            bannersRepository.Save();
        }
    }
}
