using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class BannerImagesService : IBannerImagesService
    {
        private readonly IBannerImagesRepository bannerImagesRepository;

        public BannerImagesService(IBannerImagesRepository bannerImagesRepository)
        {
            this.bannerImagesRepository = bannerImagesRepository;
        }

        public void Add(BannerImages entity)
        {
            bannerImagesRepository.Add(entity);
        }

        public void Delete(BannerImages entity)
        {
            bannerImagesRepository.Delete(entity);
        }

        public void Edit(BannerImages entity)
        {
            bannerImagesRepository.Edit(entity);
        }

        public IQueryable<BannerImages> FindBy(Expression<Func<BannerImages, bool>> predicate)
        {
            return bannerImagesRepository.FindBy(predicate);
        }

        public IQueryable<BannerImages> GetAll()
        {
            return bannerImagesRepository.GetAll().Where(x => x.Status == true);
        }

        public void Save()
        {
            bannerImagesRepository.Save();
        }
    }
}
