using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
        }

        public void Add(Vendor entity)
        {
            vendorRepository.Add(entity);
        }

        public void Delete(Vendor entity)
        {
            vendorRepository.Delete(entity);
        }

        public void Edit(Vendor entity)
        {
            vendorRepository.Edit(entity);
        }

        public IQueryable<Vendor> FindBy(Expression<Func<Vendor, bool>> predicate)
        {
            return vendorRepository.FindBy(predicate);
        }

        public IQueryable<Vendor> GetAll()
        {
            return vendorRepository.GetAll();
        }

        public void Save()
        {
            vendorRepository.Save();
        }
    }
}