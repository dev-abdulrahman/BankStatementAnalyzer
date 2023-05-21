using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
        }

        public void Add(Coupon entity)
        {
            couponRepository.Add(entity);
        }

        public void Delete(Coupon entity)
        {
            couponRepository.Delete(entity);
        }

        public void Edit(Coupon entity)
        {
            couponRepository.Edit(entity);
        }

        public IQueryable<Coupon> FindBy(Expression<Func<Coupon, bool>> predicate)
        {
            return couponRepository.FindBy(predicate);
        }

        public IQueryable<Coupon> GetAll()
        {
            return couponRepository.GetAll();
        }

        public void Save()
        {
            couponRepository.Save();
        }
    }
}