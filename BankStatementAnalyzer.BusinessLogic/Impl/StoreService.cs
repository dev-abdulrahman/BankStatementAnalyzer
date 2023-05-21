using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class StoreService : IStoreService
    {
        private readonly Lazy<IStoreRepository> storeRepository;

        public StoreService(Lazy<IStoreRepository> storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public void Add(Store entity)
        {
            storeRepository.Value.Add(entity);
        }

        public void Delete(Store entity)
        {
            storeRepository.Value.Delete(entity);
        }

        public void Edit(Store entity)
        {
            storeRepository.Value.Edit(entity);
        }

        public IQueryable<Store> FindBy(Expression<Func<Store, bool>> predicate)
        {
            return storeRepository.Value.FindBy(predicate);
        }

        public IQueryable<Store> GetAll()
        {
            return storeRepository.Value.GetAll();
        }

        public void Save()
        {
            storeRepository.Value.Save();
        }
    }
}