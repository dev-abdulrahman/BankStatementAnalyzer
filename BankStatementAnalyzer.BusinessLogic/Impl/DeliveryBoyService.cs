using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class DeliveryBoyService : IDeliveryBoyService
    {
        private readonly IDeliveryBoyRepository deliveryBoyRepository;

        public DeliveryBoyService(IDeliveryBoyRepository deliveryBoyRepository)
        {
            this.deliveryBoyRepository = deliveryBoyRepository;
        }

        public void Add(DeliveryBoy entity)
        {
            deliveryBoyRepository.Add(entity);
        }

        public void Delete(DeliveryBoy entity)
        {
            deliveryBoyRepository.Delete(entity);
        }

        public void Edit(DeliveryBoy entity)
        {
            deliveryBoyRepository.Edit(entity);
        }

        public IQueryable<DeliveryBoy> FindBy(Expression<Func<DeliveryBoy, bool>> predicate)
        {
            return deliveryBoyRepository.FindBy(predicate);
        }

        public IQueryable<DeliveryBoy> GetAll()
        {
            return deliveryBoyRepository.GetAll();
        }

        public void Save()
        {
            deliveryBoyRepository.Save();
        }
    }
}