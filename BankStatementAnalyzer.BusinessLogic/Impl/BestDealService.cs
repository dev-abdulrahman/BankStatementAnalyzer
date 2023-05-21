using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class BestDealService : IBestDealService
    {
        private readonly IBestDealRepository bestDealRepository;

        public BestDealService(IBestDealRepository bestDealRepository)
        {
            this.bestDealRepository = bestDealRepository;
        }

        public void Add(BestDeal entity)
        {
            bestDealRepository.Add(entity);
        }

        public void Delete(BestDeal entity)
        {
            bestDealRepository.Delete(entity);
        }

        public void Edit(BestDeal entity)
        {
            bestDealRepository.Edit(entity);
        }

        public IQueryable<BestDeal> FindBy(Expression<Func<BestDeal, bool>> predicate)
        {
            return bestDealRepository.FindBy(predicate);
        }

        public IQueryable<BestDeal> GetAll()
        {
            return bestDealRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            bestDealRepository.Save();
        }
    }
}
