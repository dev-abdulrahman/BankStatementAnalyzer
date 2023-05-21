using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class StyleTraitValueProductService : IStyleTraitValueProductService
    {
        private readonly IStyleTraitValueProductRepository styleTraitValueProductRepository;

        public StyleTraitValueProductService(IStyleTraitValueProductRepository styleTraitValueProductRepository)
        {
            this.styleTraitValueProductRepository = styleTraitValueProductRepository;
        }

        public void Add(StyleTraitValueProduct entity)
        {
            styleTraitValueProductRepository.Add(entity);
        }

        public void Delete(StyleTraitValueProduct entity)
        {
            styleTraitValueProductRepository.Delete(entity);
        }

        public void Edit(StyleTraitValueProduct entity)
        {
            styleTraitValueProductRepository.Edit(entity);
        }

        public IQueryable<StyleTraitValueProduct> FindBy(Expression<Func<StyleTraitValueProduct, bool>> predicate)
        {
            return styleTraitValueProductRepository.FindBy(predicate);
        }

        public IQueryable<StyleTraitValueProduct> GetAll()
        {
            return styleTraitValueProductRepository.GetAll();
        }

        public void Save()
        {
            styleTraitValueProductRepository.Save();
        }
    }
}