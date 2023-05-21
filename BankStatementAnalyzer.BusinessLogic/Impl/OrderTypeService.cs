using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class OrderTypeService : IOrderTypeService
    {
        private readonly IOrderTypeRepository orderTypeRepository;

        public OrderTypeService(IOrderTypeRepository orderTypeRepository)
        {
            this.orderTypeRepository = orderTypeRepository;
        }

        public void Add(OrderType entity)
        {
            orderTypeRepository.Add(entity);
        }

        public void Delete(OrderType entity)
        {
            orderTypeRepository.Delete(entity);
        }

        public void Edit(OrderType entity)
        {
            orderTypeRepository.Edit(entity);
        }

        public IQueryable<OrderType> FindBy(Expression<Func<OrderType, bool>> predicate)
        {
            return orderTypeRepository.FindBy(predicate);
        }

        public IQueryable<OrderType> GetAll()
        {
            return orderTypeRepository.GetAll();
        }

        public void Save()
        {
            orderTypeRepository.Save();
        }
    }
}