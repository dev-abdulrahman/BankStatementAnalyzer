using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ReturnRequestDeliveryBoyMappingService : IReturnRequestDeliveryBoyMappingService
    {
        private readonly IReturnRequestDeliveryBoyMappingRepository returnRequestDeliveryBoyMappingRepository;

        public ReturnRequestDeliveryBoyMappingService(IReturnRequestDeliveryBoyMappingRepository returnRequestDeliveryBoyMappingRepository)
        {
            this.returnRequestDeliveryBoyMappingRepository = returnRequestDeliveryBoyMappingRepository;
        }

        public void Add(ReturnRequestDeliveryBoyMapping entity)
        {
            returnRequestDeliveryBoyMappingRepository.Add(entity);
        }

        public void Delete(ReturnRequestDeliveryBoyMapping entity)
        {
            returnRequestDeliveryBoyMappingRepository.Delete(entity);
        }

        public void Edit(ReturnRequestDeliveryBoyMapping entity)
        {
            returnRequestDeliveryBoyMappingRepository.Edit(entity);
        }

        public IQueryable<ReturnRequestDeliveryBoyMapping> FindBy(Expression<Func<ReturnRequestDeliveryBoyMapping, bool>> predicate)
        {
            return returnRequestDeliveryBoyMappingRepository.FindBy(predicate);
        }

        public IQueryable<ReturnRequestDeliveryBoyMapping> GetAll()
        {
            return returnRequestDeliveryBoyMappingRepository.GetAll();
        }

        public void Save()
        {
            returnRequestDeliveryBoyMappingRepository.Save();
        }
    }
}