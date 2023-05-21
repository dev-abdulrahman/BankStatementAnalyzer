using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class StyleTraitService : IStyleTraitService
    {
        private readonly IStyleTraitRepository styleTraitRepository;

        public StyleTraitService(IStyleTraitRepository styleTraitRepository)
        {
            this.styleTraitRepository = styleTraitRepository;
        }

        public void Add(StyleTrait entity)
        {
            styleTraitRepository.Add(entity);
        }

        public void Delete(StyleTrait entity)
        {
            styleTraitRepository.Delete(entity);
        }

        public void Edit(StyleTrait entity)
        {
            styleTraitRepository.Edit(entity);
        }

        public IQueryable<StyleTrait> FindBy(Expression<Func<StyleTrait, bool>> predicate)
        {
            return styleTraitRepository.FindBy(predicate);
        }

        public IQueryable<StyleTrait> GetAll()
        {
            return styleTraitRepository.GetAll();
        }

        public void Save()
        {
            styleTraitRepository.Save();
        }
    }
}