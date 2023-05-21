using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClothsService : IClothsService
    {
        private readonly IClothsRepository clothsRepository;

        public ClothsService(IClothsRepository clothsRepository)
        {
            this.clothsRepository = clothsRepository;
        }

        public void Add(Cloths entity)
        {
            clothsRepository.Add(entity);
        }

        public void Delete(Cloths entity)
        {
            clothsRepository.Delete(entity);
        }

        public void Edit(Cloths entity)
        {
            clothsRepository.Edit(entity);
        }

        public IQueryable<Cloths> FindBy(Expression<Func<Cloths, bool>> predicate)
        {
            return clothsRepository.FindBy(predicate);
        }

        public IQueryable<Cloths> GetAll()
        {
            return clothsRepository.GetAll();
        }

        public void Save()
        {
            clothsRepository.Save();
        }
    }
}