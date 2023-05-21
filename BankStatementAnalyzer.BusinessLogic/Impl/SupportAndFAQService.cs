using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class SupportAndFAQService : ISupportAndFAQService
    {
        private readonly ISupportAndFAQRepository supportAndFAQRepository;

        public SupportAndFAQService(ISupportAndFAQRepository supportAndFAQRepository)
        {
            this.supportAndFAQRepository = supportAndFAQRepository;
        }

        public void Add(SupportAndFAQ entity)
        {
            supportAndFAQRepository.Add(entity);
        }

        public void Delete(SupportAndFAQ entity)
        {
            supportAndFAQRepository.Delete(entity);
        }

        public void Edit(SupportAndFAQ entity)
        {
            supportAndFAQRepository.Edit(entity);
        }

        public IQueryable<SupportAndFAQ> FindBy(Expression<Func<SupportAndFAQ, bool>> predicate)
        {
            return supportAndFAQRepository.FindBy(predicate);
        }

        public IQueryable<SupportAndFAQ> GetAll()
        {
            return supportAndFAQRepository.GetAll();
        }

        public void Save()
        {
            supportAndFAQRepository.Save();
        }
    }
}