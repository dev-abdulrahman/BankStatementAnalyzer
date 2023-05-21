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
    public class CustomerLikesService : ICustomerLikesService
    {
        private readonly ICustomerLikesRepository customerLikesRepository;

        public CustomerLikesService(ICustomerLikesRepository customerLikesRepository)
        {
            this.customerLikesRepository = customerLikesRepository;
        }

        public void Add(CustomerLikes entity)
        {
            customerLikesRepository.Add(entity);
        }

        public void Delete(CustomerLikes entity)
        {
            customerLikesRepository.Delete(entity);
        }

        public void Edit(CustomerLikes entity)
        {
            customerLikesRepository.Edit(entity);
        }

        public IQueryable<CustomerLikes> FindBy(Expression<Func<CustomerLikes, bool>> predicate)
        {
            return customerLikesRepository.FindBy(predicate);
        }

        public IQueryable<CustomerLikes> GetAll()
        {
            return customerLikesRepository.GetAll().Where(x => x.Status == true);
        }

        public void CustomerLikeDislike(CustomerLikes entity, Banners bannerModel)
        {
            if (entity.IsLiked)
            {
                customerLikesRepository.Add(entity);
                if (bannerModel != null)
                {
                    IncreaseLikeCount(bannerModel);
                }
            }
            else
            {
                customerLikesRepository.Delete(customerLikesRepository.FindBy(x => x.CustomerId == entity.CustomerId && x.BannerId == entity.BannerId).FirstOrDefault());
                if (bannerModel != null)
                {
                    DecreaseLikeCount(bannerModel);
                }
            }
        }

        public void IncreaseLikeCount(Banners bannerModel)
        {
            bannerModel.LikeCount += 1;
        }

        public void DecreaseLikeCount(Banners bannerModel)
        {
            bannerModel.LikeCount -= 1;
        }

        public void Save()
        {
            customerLikesRepository.Save();
        }
    }
}
