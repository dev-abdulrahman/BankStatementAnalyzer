using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly Lazy<IOrderDetailRepository> orderDetailRepository;

        public OrderDetailService(Lazy<IOrderDetailRepository> orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
        }

        public void Add(OrderDetail entity)
        {
            orderDetailRepository.Value.Add(entity);
        }

        public void Delete(OrderDetail entity)
        {
            orderDetailRepository.Value.Delete(entity);
        }

        public void Edit(OrderDetail entity)
        {
            orderDetailRepository.Value.Edit(entity);
        }

        public IQueryable<OrderDetail> FindBy(Expression<Func<OrderDetail, bool>> predicate)
        {
            return orderDetailRepository.Value.FindBy(predicate);
        }

        public IQueryable<OrderDetail> GetAll()
        {
            return orderDetailRepository.Value.GetAll();
        }

        public void Save()
        {
            orderDetailRepository.Value.Save();
        }
    }
}