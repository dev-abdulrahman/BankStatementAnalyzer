using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            this.galleryRepository = galleryRepository;
        }

        public void Add(Gallery entity)
        {
            galleryRepository.Add(entity);
        }

        public void Delete(Gallery entity)
        {
            galleryRepository.Delete(entity);
        }

        public void Edit(Gallery entity)
        {
            galleryRepository.Edit(entity);
        }

        public IQueryable<Gallery> FindBy(Expression<Func<Gallery, bool>> predicate)
        {
            return galleryRepository.FindBy(predicate);
        }

        public IQueryable<Gallery> GetAll()
        {
            return galleryRepository.GetAll();
        }

        public void Save()
        {
            galleryRepository.Save();
        }
    }
}