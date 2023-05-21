using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class StyleClassService : IStyleClassService
    {
        private readonly IStyleClassRepository styleClassRepository;

        public StyleClassService(IStyleClassRepository styleClassRepository)
        {
            this.styleClassRepository = styleClassRepository;
        }

        public void Add(StyleClass entity)
        {
            styleClassRepository.Add(entity);
        }

        public void Delete(StyleClass entity)
        {
            styleClassRepository.Delete(entity);
        }

        public void Edit(StyleClass entity)
        {
            styleClassRepository.Edit(entity);
        }

        public IQueryable<StyleClass> FindBy(Expression<Func<StyleClass, bool>> predicate)
        {
            return styleClassRepository.FindBy(predicate);
        }

        public IQueryable<StyleClass> GetAll()
        {
            return styleClassRepository.GetAll();
        }

        public void Save()
        {
            styleClassRepository.Save();
        }
    }
}