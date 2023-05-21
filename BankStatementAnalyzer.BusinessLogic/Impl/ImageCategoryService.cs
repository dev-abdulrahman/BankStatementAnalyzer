using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ImageCategoryService : IImageCategoryService
    {
        private readonly IImageCategoryRepository imageCategoryRepository;

        public ImageCategoryService(IImageCategoryRepository imageCategoryRepository)
        {
            this.imageCategoryRepository = imageCategoryRepository;
        }

        public void Add(ImageCategory entity)
        {
            imageCategoryRepository.Add(entity);
        }

        public void Delete(ImageCategory entity)
        {
            imageCategoryRepository.Delete(entity);
        }

        public void Edit(ImageCategory entity)
        {
            imageCategoryRepository.Edit(entity);
        }

        public IQueryable<ImageCategory> FindBy(Expression<Func<ImageCategory, bool>> predicate)
        {
            return imageCategoryRepository.FindBy(predicate);
        }

        public IQueryable<ImageCategory> GetAll()
        {
            return imageCategoryRepository.GetAll();
        }

        public void Save()
        {
            imageCategoryRepository.Save();
        }
    }
}