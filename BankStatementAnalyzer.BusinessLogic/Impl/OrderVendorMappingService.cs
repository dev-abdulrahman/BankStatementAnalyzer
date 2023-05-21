using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class OrderVendorMappingService : IOrderVendorMappingService
    {
        private readonly IOrderVendorMappingRepository OrderVendorMappingRepository;

        public OrderVendorMappingService(IOrderVendorMappingRepository OrderVendorMappingRepository)
        {
            this.OrderVendorMappingRepository = OrderVendorMappingRepository;
        }

        public void Add(OrderVendorMapping entity)
        {
            OrderVendorMappingRepository.Add(entity);
        }

        public void Delete(OrderVendorMapping entity)
        {
            OrderVendorMappingRepository.Delete(entity);
        }

        public void Edit(OrderVendorMapping entity)
        {
            OrderVendorMappingRepository.Edit(entity);
        }

        public IQueryable<OrderVendorMapping> FindBy(Expression<Func<OrderVendorMapping, bool>> predicate)
        {
            return OrderVendorMappingRepository.FindBy(predicate);
        }

        public IQueryable<OrderVendorMapping> GetAll()
        {
            return OrderVendorMappingRepository.GetAll();
        }

        public void Save()
        {
            OrderVendorMappingRepository.Save();
        }
    }
}