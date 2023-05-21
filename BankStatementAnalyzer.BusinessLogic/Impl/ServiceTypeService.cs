using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository serviceTypeRepository;

        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository)
        {
            this.serviceTypeRepository = serviceTypeRepository;
        }

        public void Add(ServiceType entity)
        {
            serviceTypeRepository.Add(entity);
        }

        public void Delete(ServiceType entity)
        {
            serviceTypeRepository.Delete(entity);
        }

        public void Edit(ServiceType entity)
        {
            serviceTypeRepository.Edit(entity);
        }

        public IQueryable<ServiceType> FindBy(Expression<Func<ServiceType, bool>> predicate)
        {
            return serviceTypeRepository.FindBy(predicate);
        }

        public IQueryable<ServiceType> GetAll()
        {
            return serviceTypeRepository.GetAll();
        }

        public void Save()
        {
            serviceTypeRepository.Save();
        }
    }
}