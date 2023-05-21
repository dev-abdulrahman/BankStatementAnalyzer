using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class UserMasterService : IUserMasterService
    {
        private readonly IUserMasterRepository userMasterRepository;
        private readonly IRoleMasterRepository roleMasterRepository;

        public UserMasterService(IUserMasterRepository userMasterRepository,
            IRoleMasterRepository roleMasterRepository)
        {
            this.userMasterRepository = userMasterRepository;
            this.roleMasterRepository = roleMasterRepository;
        }

        public void Add(User entity)
        {
            this.userMasterRepository.Add(entity);
        }

        public void Delete(User entity)
        {
            this.userMasterRepository.Delete(entity);
        }

        public void Edit(User entity)
        {
            this.userMasterRepository.Edit(entity);
        }

        public IQueryable<User> FindBy(Expression<Func<User, bool>> predicate)
        {
            return userMasterRepository.FindBy(predicate);
        }

        public IQueryable<User> GetAll()
        {
            return userMasterRepository.GetAll();
        }

        public Role GetRoleById(int id)
        {
            return this.roleMasterRepository.FindBy(x => x.ID == id).SingleOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return userMasterRepository.GetAll().Where(x => x.Username == username).SingleOrDefault();
        }

        public void Save()
        {
            userMasterRepository.Save();
        }
    }
}