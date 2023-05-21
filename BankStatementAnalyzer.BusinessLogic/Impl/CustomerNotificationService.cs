using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CustomerNotificationService : ICustomerNotificationService
    {
        private readonly ICustomerNotificationRepository customerNotificationRepository;

        public CustomerNotificationService(ICustomerNotificationRepository customerNotificationRepository)
        {
            this.customerNotificationRepository = customerNotificationRepository;
        }
        public void Add(CustomerNotification entity)
        {
            customerNotificationRepository.Add(entity);
        }

        public void Delete(CustomerNotification entity)
        {
            customerNotificationRepository.Delete(entity);
        }

        public void Edit(CustomerNotification entity)
        {
            customerNotificationRepository.Edit(entity);
        }

        public IQueryable<CustomerNotification> FindBy(Expression<Func<CustomerNotification, bool>> predicate)
        {
            return customerNotificationRepository.FindBy(predicate);
        }

        public IQueryable<CustomerNotification> GetAll()
        {
            return customerNotificationRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            customerNotificationRepository.Save();
        }
    }
}
