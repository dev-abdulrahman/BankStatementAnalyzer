using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ClassifiedContactsService : IClassifiedContactsService
    {
        private readonly IClassifiedContactsRepository classifiedContactsRepository ;

        public ClassifiedContactsService(IClassifiedContactsRepository classifiedContactsRepository)
        {
            this.classifiedContactsRepository = classifiedContactsRepository;
        }
        public void Add(ClassifiedContacts entity)
        {
            classifiedContactsRepository.Add(entity);
        }

        public void Delete(ClassifiedContacts entity)
        {
            classifiedContactsRepository.Delete(entity);
        }

        public void Edit(ClassifiedContacts entity)
        {
            classifiedContactsRepository.Edit(entity);
        }

        public IQueryable<ClassifiedContacts> FindBy(Expression<Func<ClassifiedContacts, bool>> predicate)
        {
            return classifiedContactsRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedContacts> GetAll()
        {
            return classifiedContactsRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            classifiedContactsRepository.Save();
        }
    }
}
