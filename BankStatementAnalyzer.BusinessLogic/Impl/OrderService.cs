using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class OrderService : SearchService<Order>, IOrderService
    {
        private readonly Lazy<IOrderRepository> orderRepository;

        public OrderService(IGenericRepository<Order> genericRepository,
            Lazy<IOrderRepository> orderRepository) : base(genericRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void Add(Order entity)
        {
            orderRepository.Value.Add(entity);
        }

        public void Delete(Order entity)
        {
            orderRepository.Value.Delete(entity);
        }

        public void Edit(Order entity)
        {
            orderRepository.Value.Edit(entity);
        }

        public IQueryable<Order> FindBy(Expression<Func<Order, bool>> predicate)
        {
            return orderRepository.Value.FindBy(predicate);
        }

        public IQueryable<Order> GetAll()
        {
            return orderRepository.Value.GetAll();
        }

        public void Save()
        {
            orderRepository.Value.Save();
        }
    }
}