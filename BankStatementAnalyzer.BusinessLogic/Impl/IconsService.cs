using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class IconsService : IIconsService
    {
        private readonly IIconsRepository iconsRepository;

        public IconsService(IIconsRepository iconsRepository)
        {
            this.iconsRepository = iconsRepository;
        }

        public void Add(Icons entity)
        {
            iconsRepository.Add(entity);
        }

        public void Delete(Icons entity)
        {
            iconsRepository.Delete(entity);
        }

        public void Edit(Icons entity)
        {
            iconsRepository.Edit(entity);
        }

        public IQueryable<Icons> FindBy(Expression<Func<Icons, bool>> predicate)
        {
            return iconsRepository.FindBy(predicate);
        }

        public IQueryable<Icons> GetAll()
        {
            return iconsRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            iconsRepository.Save();
        }
    }
}
