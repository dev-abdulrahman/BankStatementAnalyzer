using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public void Add(Email entity)
        {
            emailRepository.Add(entity);
        }

        public void Delete(Email entity)
        {
            emailRepository.Delete(entity);
        }

        public void Edit(Email entity)
        {
            emailRepository.Edit(entity);
        }

        public IQueryable<Email> FindBy(Expression<Func<Email, bool>> predicate)
        {
            return emailRepository.FindBy(predicate);
        }

        public IQueryable<Email> GetAll()
        {
            return emailRepository.GetAll();
        }

        public void Save()
        {
            emailRepository.Save();
        }
    }
}