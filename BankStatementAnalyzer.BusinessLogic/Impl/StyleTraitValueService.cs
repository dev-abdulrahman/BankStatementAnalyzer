using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class StyleTraitValueService : IStyleTraitValueService
    {
        private readonly IStyleTraitValueRepository styleTraitValueRepository;

        public StyleTraitValueService(IStyleTraitValueRepository styleTraitValueRepository)
        {
            this.styleTraitValueRepository = styleTraitValueRepository;
        }

        public void Add(StyleTraitValue entity)
        {
            styleTraitValueRepository.Add(entity);
        }

        public void Delete(StyleTraitValue entity)
        {
            styleTraitValueRepository.Delete(entity);
        }

        public void Edit(StyleTraitValue entity)
        {
            styleTraitValueRepository.Edit(entity);
        }

        public IQueryable<StyleTraitValue> FindBy(Expression<Func<StyleTraitValue, bool>> predicate)
        {
            return styleTraitValueRepository.FindBy(predicate);
        }

        public IQueryable<StyleTraitValue> GetAll()
        {
            return styleTraitValueRepository.GetAll();
        }

        public void Save()
        {
            styleTraitValueRepository.Save();
        }
    }
}