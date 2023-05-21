using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public void Add(Address entity)
        {
            addressRepository.Add(entity);
        }

        public void Delete(Address entity)
        {
            addressRepository.Delete(entity);
        }

        public void Edit(Address entity)
        {
            addressRepository.Edit(entity);
        }

        public IQueryable<Address> FindBy(Expression<Func<Address, bool>> predicate)
        {
            return addressRepository.FindBy(predicate);
        }

        public IQueryable<Address> GetAll()
        {
            return addressRepository.GetAll();
        }

        public void Save()
        {
            addressRepository.Save();
        }
    }
}