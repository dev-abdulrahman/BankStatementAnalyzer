using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            this.packageRepository = packageRepository;
        }

        public void Add(Package entity)
        {
            packageRepository.Add(entity);
        }

        public void Delete(Package entity)
        {
            packageRepository.Delete(entity);
        }

        public void Edit(Package entity)
        {
            packageRepository.Edit(entity);
        }

        public IQueryable<Package> FindBy(Expression<Func<Package, bool>> predicate)
        {
            return packageRepository.FindBy(predicate);
        }

        public IQueryable<Package> GetAll()
        {
            return packageRepository.GetAll();
        }

        public Tuple<decimal, decimal> GetDiscount(decimal rate, decimal urgentRate, decimal discount, DiscountType discountType)
        {
            decimal amount;
            decimal urgentAmount;
            if (discountType == DiscountType.Fixed)
            {
                amount = rate - discount;
                urgentAmount = urgentRate - discount;

                return new Tuple<decimal, decimal>(amount, urgentAmount);
            }

            if (discountType == DiscountType.Percent)
            {
                var percentAmount = (rate * discount) / 100;
                amount = rate - percentAmount;
                urgentAmount = urgentRate - percentAmount;

                return new Tuple<decimal, decimal>(amount, urgentAmount);
            }

            return new Tuple<decimal, decimal>(0, 0);
        }

        public void Save()
        {
            packageRepository.Save();
        }
    }
}