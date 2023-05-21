using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class AreaManagerService : IAreaManagerService
    {
        private readonly IAreaManagerRepository areaManagerRepository;

        public AreaManagerService(IAreaManagerRepository areaManagerRepository)
        {
            this.areaManagerRepository = areaManagerRepository;
        }

        public void Add(AreaManager entity)
        {
            areaManagerRepository.Add(entity);
        }

        public void Delete(AreaManager entity)
        {
            areaManagerRepository.Delete(entity);
        }

        public void Edit(AreaManager entity)
        {
            areaManagerRepository.Edit(entity);
        }

        public IQueryable<AreaManager> FindBy(Expression<Func<AreaManager, bool>> predicate)
        {
            return areaManagerRepository.FindBy(predicate);
        }

        public IQueryable<AreaManager> GetAll()
        {
            return areaManagerRepository.GetAll();
        }

        public void Save()
        {
            areaManagerRepository.Save();
        }
    }
}