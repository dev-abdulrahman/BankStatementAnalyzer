using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class SystemSettingService : ISystemSettingService
    {
        private readonly Lazy<ISystemSettingRepository> systemSettingRepository;

        public SystemSettingService(Lazy<ISystemSettingRepository> systemSettingRepository)
        {
            this.systemSettingRepository = systemSettingRepository;
        }

        public void Add(SystemSetting entity)
        {
            systemSettingRepository.Value.Add(entity);
        }

        public void Delete(SystemSetting entity)
        {
            systemSettingRepository.Value.Delete(entity);
        }

        public void Edit(SystemSetting entity)
        {
            systemSettingRepository.Value.Edit(entity);
        }

        public IQueryable<SystemSetting> FindBy(Expression<Func<SystemSetting, bool>> predicate)
        {
            return systemSettingRepository.Value.FindBy(predicate);
        }

        public bool IsExist(string name)
        {
            return systemSettingRepository.Value.GetAll().Where(x => x.SettingTypeValue == name).Any();
        }

        public bool IsDuplicate(string name,string main)
        {

            return systemSettingRepository.Value.GetAll().Where(x => x.SettingTypeValue != main).Any(x => x.SettingTypeValue == name);

        }

        public IQueryable<SystemSetting> GetAll()
        {
            return systemSettingRepository.Value.GetAll();
        }

        public void Save()
        {
            systemSettingRepository.Value.Save();
        }
    }
}