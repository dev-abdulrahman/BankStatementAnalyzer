using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class RateCardService : IRateCardService
    {
        private readonly IRateCardRepository rateCardRepository;

        public RateCardService(IRateCardRepository rateCardRepository)
        {
            this.rateCardRepository = rateCardRepository;
        }

        public void Add(RateCard entity)
        {
            rateCardRepository.Add(entity);
        }

        public void Delete(RateCard entity)
        {
            rateCardRepository.Delete(entity);
        }

        public void Edit(RateCard entity)
        {
            rateCardRepository.Edit(entity);
        }

        public IQueryable<RateCard> FindBy(Expression<Func<RateCard, bool>> predicate)
        {
            return rateCardRepository.FindBy(predicate);
        }

        public IQueryable<RateCard> GetAll()
        {
            return rateCardRepository.GetAll();
        }

        public void Save()
        {
            rateCardRepository.Save();
        }
    }
}