using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void Add(Customer entity)
        {
            customerRepository.Add(entity);
        }

        public void Delete(Customer entity)
        {
            customerRepository.Delete(entity);
        }

        public void Edit(Customer entity)
        {
            customerRepository.Edit(entity);
        }

        public IQueryable<Customer> FindBy(Expression<Func<Customer, bool>> predicate)
        {
            return customerRepository.FindBy(predicate);
        }

        public IQueryable<Customer> GetAll()
        {
            return customerRepository.GetAll();
        }

        public void Save()
        {
            customerRepository.Save();
        }
    }
}