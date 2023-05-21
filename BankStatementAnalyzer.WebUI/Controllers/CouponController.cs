using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [AllowAnonymous]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class CouponController : BaseController
    {
        private readonly ICouponService couponService;
        private readonly IPackageService packageService;
        private readonly IPackageCouponMappingService packageCouponMappingService;

        public CouponController(ICouponService couponService,
            ILoggerFacade<BaseController> logger,
            IUserMasterService userMasterService,
            IPackageService packageService,
            IPackageCouponMappingService packageCouponMappingService) : base(userMasterService, logger)
        {
            this.couponService = couponService;
            this.packageService = packageService;
            this.packageCouponMappingService = packageCouponMappingService;
        }

        // GET: Coupon
        public ActionResult Index()
        {
            return View(couponService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Load();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                Load();
                return View(coupon);
            }


            if (couponService.FindBy(x => x.Name == coupon.Name && x.Code == coupon.Code).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                Load();
                return View(coupon);
            }

            coupon.CreatedBy = UserName;

            couponService.Add(coupon);
            couponService.Save();

            PackageCouponMapping packageCouponMapping = new PackageCouponMapping
            {
                CouponId = coupon.ID,
                PackageId = coupon.PackageId
            };

            packageCouponMappingService.Add(packageCouponMapping);
            packageCouponMappingService.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = couponService.FindBy(x => x.ID == id).FirstOrDefault();

            var dbPackageCouponMapping = packageCouponMappingService.FindBy(x => x.CouponId == id).FirstOrDefault();
            model.PackageId = dbPackageCouponMapping != null ? dbPackageCouponMapping.PackageId : 0;

            Load();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                Load();
                return View(coupon);
            }

            var oldCoupon = couponService.FindBy(x => x.ID == id).FirstOrDefault();


            if ((oldCoupon.Name != coupon.Name && oldCoupon.Code != coupon.Code) &&
                couponService.GetAll().Any(x => x.Name == coupon.Name && x.Code == coupon.Code))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                Load();
                return View(coupon);
            }

            oldCoupon.Code = coupon.Code;
            oldCoupon.Name = coupon.Name;
            oldCoupon.Description = coupon.Description;
            oldCoupon.CustomerRedemption = coupon.CustomerRedemption;
            oldCoupon.Redemption = coupon.Redemption;
            oldCoupon.MinTotal = coupon.MinTotal;
            oldCoupon.Status = coupon.Status;
            oldCoupon.DiscountType = coupon.DiscountType;
            oldCoupon.Discount = coupon.Discount;
            oldCoupon.UpdatedBy = UserName;
            oldCoupon.UpdatedDate = DateTime.Now;

            couponService.Edit(oldCoupon);
            couponService.Save();

            var dbPackageCouponMapping = packageCouponMappingService.FindBy(x => x.CouponId == id).FirstOrDefault();
            if (dbPackageCouponMapping == null)
            {
                PackageCouponMapping packageCouponMapping = new PackageCouponMapping
                {
                    CouponId = oldCoupon.ID,
                    PackageId = coupon.PackageId
                };

                packageCouponMappingService.Add(packageCouponMapping);
                packageCouponMappingService.Save();
            }
            else
            {
                dbPackageCouponMapping.PackageId = coupon.PackageId;

                packageCouponMappingService.Edit(dbPackageCouponMapping);
                packageCouponMappingService.Save();
            }

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var coupon = couponService.FindBy(x => x.ID == id).FirstOrDefault();

            return View(coupon);
        }

        [HttpPost]
        public ActionResult Delete(int id, Coupon coupon)
        {
            var model = couponService.FindBy(x => x.ID == id).FirstOrDefault();

            couponService.Delete(model);
            couponService.Save();

            var packageCouponMappings = packageCouponMappingService.FindBy(x => x.CouponId == id).ToList();

            foreach (var packageCouponMapping in packageCouponMappings)
            {
                packageCouponMappingService.Delete(packageCouponMapping);
                packageCouponMappingService.Save();
            }

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;
            return RedirectToAction("Index");
        }

        private void Load()
        {
            ViewBag.Package = packageService.GetAll().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
        }
    }
}