using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class CouponController : ApiController
    {
        private readonly ICouponService couponService;
        private readonly ICustomerService customerService;
        private readonly IPackageCouponMappingService packageCouponMappingService;
        private readonly IPackageService packageService;

        public CouponController(ICouponService couponService,
            ICustomerService customerService,
            IPackageCouponMappingService packageCouponMappingService,
            IPackageService packageService)
        {
            this.couponService = couponService;
            this.customerService = customerService;
            this.packageCouponMappingService = packageCouponMappingService;
            this.packageService = packageService;
        }

        [Route("api/Coupon/Get")]
        [HttpGet]
        public HttpResponseMessage Get(int packageId)
        {
            var mappedPackageIds = packageCouponMappingService.FindBy(x => x.PackageId == packageId).Select(x => x.CouponId).ToList();

            List<ViewModels.Api.Coupon> list = new List<ViewModels.Api.Coupon>();

            foreach (var mappedPackageId in mappedPackageIds)
            {
                var coupon = couponService.FindBy(x => x.Status == true && x.ID == mappedPackageId).FirstOrDefault();
                if (coupon != null)
                {
                    ViewModels.Api.Coupon cpn = new ViewModels.Api.Coupon
                    {
                        Id = coupon.ID,
                        Code = coupon.Code,
                        Name = coupon.Name,
                        CustomerRedemption = coupon.CustomerRedemption,
                        Description = coupon.Description,
                        Discount = coupon.Discount,
                        MinTotal = coupon.MinTotal,
                        Redemption = coupon.Redemption,
                        DiscountType = coupon.DiscountType.ToString()
                    };

                    list.Add(cpn);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("api/Coupon/GetPrice")]
        public HttpResponseMessage GetPrice(ViewModels.Api.CouponMap couponMap)
        {
            var coupon = new Coupon();

            if (couponMap.CouponId != 0)
            {
                coupon = couponService.FindBy(x => x.ID == couponMap.CouponId).FirstOrDefault();
            }
            else
            {
                coupon = couponService.FindBy(x => x.Code == couponMap.CouponCode).FirstOrDefault();
            }

            if (coupon == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Coupon not found." });
            }

            var user = customerService.FindBy(x => x.UID == couponMap.CustomerUId).FirstOrDefault();

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            var pack = packageService.FindBy(x => x.ID == couponMap.PackageId).FirstOrDefault();

            if (pack == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Package not found." });
            }

            Tuple<decimal, decimal> rate = packageService.GetDiscount(pack.Rate, pack.UrgentRate, coupon.Discount, coupon.DiscountType);

            return Request.CreateResponse(HttpStatusCode.OK, new { FinalPrice = rate.Item1, FinalUrgentPrice = rate.Item2 });
        }
    }
}