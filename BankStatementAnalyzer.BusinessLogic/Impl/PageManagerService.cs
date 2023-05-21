using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class PageManagerService : IPageManagerService
    {
        private readonly IPageManagerRepository pageManagerRepository;

        public PageManagerService(IPageManagerRepository pageManagerRepository)
        {
            this.pageManagerRepository = pageManagerRepository;
        }

        public void Add(PageManager entity)
        {
            pageManagerRepository.Add(entity);
        }

        public void Delete(PageManager entity)
        {
            pageManagerRepository.Delete(entity);
        }

        public void Edit(PageManager entity)
        {
            pageManagerRepository.Edit(entity);
        }

        public IQueryable<PageManager> FindBy(Expression<Func<PageManager, bool>> predicate)
        {
            return pageManagerRepository.FindBy(predicate);
        }

        public IQueryable<PageManager> GetAll()
        {
            return pageManagerRepository.GetAll();
        }

        public void Save()
        {
            pageManagerRepository.Save();
        }
    }
}