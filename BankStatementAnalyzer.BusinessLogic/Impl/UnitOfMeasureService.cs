using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly IUnitOfMeasureRepository unitOfMeasureRepository;

        public UnitOfMeasureService(IUnitOfMeasureRepository unitOfMeasureRepository)
        {
            this.unitOfMeasureRepository = unitOfMeasureRepository;
        }

        public void Add(UnitOfMeasure entity)
        {
            unitOfMeasureRepository.Add(entity);
        }

        public void Delete(UnitOfMeasure entity)
        {
            unitOfMeasureRepository.Delete(entity);
        }

        public void Edit(UnitOfMeasure entity)
        {
            unitOfMeasureRepository.Edit(entity);
        }

        public IQueryable<UnitOfMeasure> FindBy(Expression<Func<UnitOfMeasure, bool>> predicate)
        {
            return unitOfMeasureRepository.FindBy(predicate);
        }

        public IQueryable<UnitOfMeasure> GetAll()
        {
            return unitOfMeasureRepository.GetAll();
        }

        public void Save()
        {
            unitOfMeasureRepository.Save();
        }
    }
}