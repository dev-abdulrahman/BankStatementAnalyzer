using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public void Add(Payment entity)
        {
            paymentRepository.Add(entity);
        }

        public void Delete(Payment entity)
        {
            paymentRepository.Delete(entity);
        }

        public void Edit(Payment entity)
        {
            paymentRepository.Edit(entity);
        }

        public IQueryable<Payment> FindBy(Expression<Func<Payment, bool>> predicate)
        {
            return paymentRepository.FindBy(predicate);
        }

        public IQueryable<Payment> GetAll()
        {
            return paymentRepository.GetAll();
        }

        public void Save()
        {
            paymentRepository.Save();
        }
    }
}