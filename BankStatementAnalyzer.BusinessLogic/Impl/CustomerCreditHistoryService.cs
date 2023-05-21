using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CustomerCreditHistoryService : ICustomerCreditHistoryService
    {
        private readonly ICustomerCreditHistoryRepository customerCreditHistoryRepository;

        public CustomerCreditHistoryService(ICustomerCreditHistoryRepository customerCreditHistoryRepository)
        {
            this.customerCreditHistoryRepository = customerCreditHistoryRepository;
        }

        public void Add(CustomerCreditHistory entity)
        {
            customerCreditHistoryRepository.Add(entity);
        }

        public void Delete(CustomerCreditHistory entity)
        {
            customerCreditHistoryRepository.Delete(entity);
        }

        public void Edit(CustomerCreditHistory entity)
        {
            customerCreditHistoryRepository.Edit(entity);
        }

        public IQueryable<CustomerCreditHistory> FindBy(Expression<Func<CustomerCreditHistory, bool>> predicate)
        {
            return customerCreditHistoryRepository.FindBy(predicate);
        }

        public IQueryable<CustomerCreditHistory> GetAll()
        {
            return customerCreditHistoryRepository.GetAll();
        }

        public void Save()
        {
            customerCreditHistoryRepository.Save();
        }
    }
}