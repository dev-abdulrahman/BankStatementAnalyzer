using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class StreetService : IStreetService
    {
        private readonly IStreetRepository streetRepository;

        public StreetService(IStreetRepository streetRepository)
        {
            this.streetRepository = streetRepository;
        }

        public void Add(Street entity)
        {
            streetRepository.Add(entity);
        }

        public void Delete(Street entity)
        {
            streetRepository.Delete(entity);
        }

        public void Edit(Street entity)
        {
            streetRepository.Edit(entity);
        }

        public IQueryable<Street> FindBy(Expression<Func<Street, bool>> predicate)
        {
            return streetRepository.FindBy(predicate);
        }

        public IQueryable<Street> GetAll()
        {
            return streetRepository.GetAll();
        }

        public void Save()
        {
            streetRepository.Save();
        }
    }
}