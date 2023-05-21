using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class OrderDelvieryBoyMappingService : IOrderDelvieryBoyMappingService
    {
        private readonly IOrderDelvieryBoyMappingRepository orderDelvieryBoyMappingRepository;

        public OrderDelvieryBoyMappingService(IOrderDelvieryBoyMappingRepository orderDelvieryBoyMappingRepository)
        {
            this.orderDelvieryBoyMappingRepository = orderDelvieryBoyMappingRepository;
        }

        public void Add(OrderDelvieryBoyMapping entity)
        {
            orderDelvieryBoyMappingRepository.Add(entity);
        }

        public void Delete(OrderDelvieryBoyMapping entity)
        {
            orderDelvieryBoyMappingRepository.Delete(entity);
        }

        public void Edit(OrderDelvieryBoyMapping entity)
        {
            orderDelvieryBoyMappingRepository.Edit(entity);
        }

        public IQueryable<OrderDelvieryBoyMapping> FindBy(Expression<Func<OrderDelvieryBoyMapping, bool>> predicate)
        {
            return orderDelvieryBoyMappingRepository.FindBy(predicate);
        }

        public IQueryable<OrderDelvieryBoyMapping> GetAll()
        {
            return orderDelvieryBoyMappingRepository.GetAll();
        }

        public void Save()
        {
            orderDelvieryBoyMappingRepository.Save();
        }
    }
}