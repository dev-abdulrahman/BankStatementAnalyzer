using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CustomerCreditMappingService : ICustomerCreditMappingService
    {
        private readonly ICustomerCreditMappingRepository customerCreditMappingRepository;

        public CustomerCreditMappingService(ICustomerCreditMappingRepository customerCreditMappingRepository)
        {
            this.customerCreditMappingRepository = customerCreditMappingRepository;
        }

        public void Add(CustomerCreditMapping entity)
        {
            customerCreditMappingRepository.Add(entity);
        }

        public void Delete(CustomerCreditMapping entity)
        {
            customerCreditMappingRepository.Delete(entity);
        }

        public void Edit(CustomerCreditMapping entity)
        {
            customerCreditMappingRepository.Edit(entity);
        }

        public IQueryable<CustomerCreditMapping> FindBy(Expression<Func<CustomerCreditMapping, bool>> predicate)
        {
            return customerCreditMappingRepository.FindBy(predicate);
        }

        public IQueryable<CustomerCreditMapping> GetAll()
        {
            return customerCreditMappingRepository.GetAll();
        }

        public void Save()
        {
            customerCreditMappingRepository.Save();
        }
    }
}