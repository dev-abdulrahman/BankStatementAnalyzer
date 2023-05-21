using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ColorsService : IColorsService
    {
        private readonly IColorsRepository colorsRepository;

        public ColorsService(IColorsRepository colorsRepository)
        {
            this.colorsRepository = colorsRepository;
        }

        public void Add(Colors entity)
        {
            colorsRepository.Add(entity);
        }

        public void Delete(Colors entity)
        {
            colorsRepository.Delete(entity);
        }

        public void Edit(Colors entity)
        {
            colorsRepository.Edit(entity);
        }

        public IQueryable<Colors> FindBy(Expression<Func<Colors, bool>> predicate)
        {
            return colorsRepository.FindBy(predicate);
        }

        public IQueryable<Colors> GetAll()
        {
            return colorsRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            colorsRepository.Save();
        }
    }
}
