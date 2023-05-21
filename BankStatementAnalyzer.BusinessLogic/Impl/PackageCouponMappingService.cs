using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class PackageCouponMappingService : IPackageCouponMappingService
    {
        private readonly IPackageCouponMappingRepository pPackageCouponMappingRepository;

        public PackageCouponMappingService(IPackageCouponMappingRepository pPackageCouponMappingRepository)
        {
            this.pPackageCouponMappingRepository = pPackageCouponMappingRepository;
        }

        public void Add(PackageCouponMapping entity)
        {
            pPackageCouponMappingRepository.Add(entity);
        }

        public void Delete(PackageCouponMapping entity)
        {
            pPackageCouponMappingRepository.Delete(entity);
        }

        public void Edit(PackageCouponMapping entity)
        {
            pPackageCouponMappingRepository.Edit(entity);
        }

        public IQueryable<PackageCouponMapping> FindBy(Expression<Func<PackageCouponMapping, bool>> predicate)
        {
            return pPackageCouponMappingRepository.FindBy(predicate);
        }

        public IQueryable<PackageCouponMapping> GetAll()
        {
            return pPackageCouponMappingRepository.GetAll();
        }

        public void Save()
        {
            pPackageCouponMappingRepository.Save();
        }
    }
}