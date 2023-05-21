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
    public class ClassifiedImagesService : IClassifiedImagesService
    {
        private readonly IClassifiedImagesRepository classifiedImagesRepository;

        public ClassifiedImagesService(IClassifiedImagesRepository classifiedImagesRepository)
        {
            this.classifiedImagesRepository = classifiedImagesRepository;
        }
        public void Add(ClassifiedImages entity)
        {
            classifiedImagesRepository.Add(entity);
        }

        public void Delete(ClassifiedImages entity)
        {
            classifiedImagesRepository.Delete(entity);
        }

        public void Edit(ClassifiedImages entity)
        {
            classifiedImagesRepository.Edit(entity);
        }

        public IQueryable<ClassifiedImages> FindBy(Expression<Func<ClassifiedImages, bool>> predicate)
        {
            return classifiedImagesRepository.FindBy(predicate);
        }

        public IQueryable<ClassifiedImages> GetAll()
        {
            return classifiedImagesRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            classifiedImagesRepository.Save();
        }
    }
}
