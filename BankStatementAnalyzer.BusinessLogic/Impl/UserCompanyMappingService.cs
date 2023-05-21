using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class UserCompanyMappingService : IUserCompanyMappingService
    {
        private readonly IUserCompanyMappingRepository userCompanyMappingRepository;

        public UserCompanyMappingService(IUserCompanyMappingRepository userCompanyMappingRepository)
        {
            this.userCompanyMappingRepository = userCompanyMappingRepository;
        }

        public void Add(UserCompanyMapping entity)
        {
            userCompanyMappingRepository.Add(entity);
        }

        public void Delete(UserCompanyMapping entity)
        {
            userCompanyMappingRepository.Delete(entity);
        }

        public void Edit(UserCompanyMapping entity)
        {
            userCompanyMappingRepository.Edit(entity);
        }

        public IQueryable<UserCompanyMapping> FindBy(Expression<Func<UserCompanyMapping, bool>> predicate)
        {
            return userCompanyMappingRepository.FindBy(predicate);
        }

        public IQueryable<UserCompanyMapping> GetAll()
        {
            return userCompanyMappingRepository.GetAll();
        }

        public void Save()
        {
            userCompanyMappingRepository.Save();
        }
    }
}