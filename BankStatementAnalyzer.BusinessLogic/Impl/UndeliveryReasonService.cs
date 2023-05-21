using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class UndeliveryReasonService : IUndeliveryReasonService
    {
        private readonly IUndeliveryReasonRepository undeliveryReasonRepository;

        public UndeliveryReasonService(IUndeliveryReasonRepository undeliveryReasonRepository)
        {
            this.undeliveryReasonRepository = undeliveryReasonRepository;
        }

        public void Add(UndeliveryReason entity)
        {
            undeliveryReasonRepository.Add(entity);
        }

        public void Delete(UndeliveryReason entity)
        {
            undeliveryReasonRepository.Delete(entity);
        }

        public void Edit(UndeliveryReason entity)
        {
            undeliveryReasonRepository.Edit(entity);
        }

        public IQueryable<UndeliveryReason> FindBy(Expression<Func<UndeliveryReason, bool>> predicate)
        {
            return undeliveryReasonRepository.FindBy(predicate);
        }

        public IQueryable<UndeliveryReason> GetAll()
        {
            return undeliveryReasonRepository.GetAll();
        }

        public void Save()
        {
            undeliveryReasonRepository.Save();
        }
    }
}