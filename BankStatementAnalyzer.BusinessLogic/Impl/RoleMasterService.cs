using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class RoleMasterService : IRoleMasterService
    {
        private readonly IRoleMasterRepository roleMasterRepository;
        private readonly IPermissionMasterRepository permissionMasterRepository;

        public RoleMasterService(IRoleMasterRepository roleMasterRepository,
            IPermissionMasterRepository permissionMasterRepository)
        {
            this.roleMasterRepository = roleMasterRepository;
            this.permissionMasterRepository = permissionMasterRepository;
        }

        public void Add(Role entity)
        {
            this.roleMasterRepository.Add(entity);
        }

        public void Delete(Role entity)
        {
            this.roleMasterRepository.Delete(entity);
        }

        public void Edit(Role entity)
        {
            this.roleMasterRepository.Edit(entity);
        }

        public IQueryable<Role> FindBy(Expression<Func<Role, bool>> predicate)
        {
            return roleMasterRepository.FindBy(predicate);
        }

        public IQueryable<Role> GetAll()
        {
            return roleMasterRepository.GetAll();
        }

        public Permission GetPermissionById(int id)
        {
            return permissionMasterRepository.FindBy(c => c.ID == id).SingleOrDefault();
        }

        public Role GetRoleByName(string name, int id)
        {
            return roleMasterRepository.FindBy(c => c.Name == name && c.ID != id).SingleOrDefault();
        }

        public void Save()
        {
            roleMasterRepository.Save();
        }
    }
}