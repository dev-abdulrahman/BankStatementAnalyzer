using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class PermissionMasterService : IPermissionMasterService
    {
        private readonly IPermissionMasterRepository permissionMasterRepository;
        private readonly IUserMasterRepository userMasterRepository;

        public PermissionMasterService(IPermissionMasterRepository permissionMasterRepository,
            IUserMasterRepository userMasterRepository)
        {
            this.permissionMasterRepository = permissionMasterRepository;
            this.userMasterRepository = userMasterRepository;
        }

        public void Add(Permission entity)
        {
            this.permissionMasterRepository.Add(entity);
        }

        public void Delete(Permission entity)
        {
            this.permissionMasterRepository.Delete(entity);
        }

        public void Edit(Permission entity)
        {
            this.permissionMasterRepository.Edit(entity);
        }

        public IQueryable<Permission> FindBy(Expression<Func<Permission, bool>> predicate)
        {
            return permissionMasterRepository.FindBy(predicate);
        }

        public IQueryable<Permission> GetAll()
        {
            return permissionMasterRepository.GetAll();
        }

        public List<string> GetAllForUser(string username)
        {
            User user = userMasterRepository.FindBy(u => u.Username == username).SingleOrDefault();
            List<string> permissions = new List<string>();

            List<Role> roles = user.Roles.ToList();
            foreach (Role role in roles)
            {
                foreach (Permission permission in role.Permissions)
                {
                    foreach (Models.Action action in permission.Actions)
                    {
                        if (!permissions.Contains(action.Name))
                            permissions.Add(action.Name);
                    }
                }
            }
            return permissions;
        }

        public void Save()
        {
            permissionMasterRepository.Save();
        }
    }
}