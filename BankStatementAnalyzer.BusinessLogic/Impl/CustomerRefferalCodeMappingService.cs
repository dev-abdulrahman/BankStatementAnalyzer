using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CustomerRefferalCodeMappingService : ICustomerRefferalCodeMappingService
    {
        private readonly ICustomerRefferalCodeMappingRepository customerRefferalCodeMappingRepository;

        public CustomerRefferalCodeMappingService(ICustomerRefferalCodeMappingRepository customerRefferalCodeMappingRepository)
        {
            this.customerRefferalCodeMappingRepository = customerRefferalCodeMappingRepository;
        }

        public void Add(CustomerRefferalCodeMapping entity)
        {
            customerRefferalCodeMappingRepository.Add(entity);
        }

        public void Delete(CustomerRefferalCodeMapping entity)
        {
            customerRefferalCodeMappingRepository.Delete(entity);
        }

        public void Edit(CustomerRefferalCodeMapping entity)
        {
            customerRefferalCodeMappingRepository.Edit(entity);
        }

        public IQueryable<CustomerRefferalCodeMapping> FindBy(Expression<Func<CustomerRefferalCodeMapping, bool>> predicate)
        {
            return customerRefferalCodeMappingRepository.FindBy(predicate);
        }

        public IQueryable<CustomerRefferalCodeMapping> GetAll()
        {
            return customerRefferalCodeMappingRepository.GetAll();
        }

        public void Save()
        {
            customerRefferalCodeMappingRepository.Save();
        }
    }
}