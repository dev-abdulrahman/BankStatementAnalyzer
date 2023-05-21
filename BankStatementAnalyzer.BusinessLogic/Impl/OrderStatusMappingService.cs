using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class OrderStatusMappingService : IOrderStatusMappingService
    {
        private readonly IOrderStatusMappingRepository orderStatusMappingRepository;

        public OrderStatusMappingService(IOrderStatusMappingRepository orderStatusMappingRepository)
        {
            this.orderStatusMappingRepository = orderStatusMappingRepository;
        }

        public void Add(OrderStatusMapping entity)
        {
            orderStatusMappingRepository.Add(entity);
        }

        public void Delete(OrderStatusMapping entity)
        {
            orderStatusMappingRepository.Delete(entity);
        }

        public void Edit(OrderStatusMapping entity)
        {
            orderStatusMappingRepository.Edit(entity);
        }

        public IQueryable<OrderStatusMapping> FindBy(Expression<Func<OrderStatusMapping, bool>> predicate)
        {
            return orderStatusMappingRepository.FindBy(predicate);
        }

        public IQueryable<OrderStatusMapping> GetAll()
        {
            return orderStatusMappingRepository.GetAll();
        }

        public void Save()
        {
            orderStatusMappingRepository.Save();
        }
    }
}