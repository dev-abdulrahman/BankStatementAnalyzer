using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class AppMessageService : IAppMessageService
    {
        private readonly IAppMessageRepository appMessageRepository;

        public AppMessageService(IAppMessageRepository appMessageRepository)
        {
            this.appMessageRepository = appMessageRepository;
        }

        public void Add(AppMessage entity)
        {
            appMessageRepository.Add(entity);
        }

        public void Delete(AppMessage entity)
        {
            appMessageRepository.Delete(entity);
        }

        public void Edit(AppMessage entity)
        {
            appMessageRepository.Edit(entity);
        }

        public IQueryable<AppMessage> FindBy(Expression<Func<AppMessage, bool>> predicate)
        {
            return appMessageRepository.FindBy(predicate);
        }

        public IQueryable<AppMessage> GetAll()
        {
            return appMessageRepository.GetAll();
        }

        public void Save()
        {
            appMessageRepository.Save();
        }
    }
}