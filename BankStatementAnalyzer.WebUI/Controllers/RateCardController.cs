using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class RateCardController : BaseController
    {
        private readonly Lazy<IRateCardService> rateCardService;
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<IServiceTypeService> serviceTypeService;
        private readonly Lazy<IClothsService> clothsService;

        public RateCardController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<IRateCardService> rateCardService,
            Lazy<ICategoryService> categoryService,
            Lazy<IServiceTypeService> serviceTypeService,
            Lazy<IClothsService> clothsService) : base(userMasterService.Value, logger.Value)
        {
            this.rateCardService = rateCardService;
            this.categoryService = categoryService;
            this.serviceTypeService = serviceTypeService;
            this.clothsService = clothsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(rateCardService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Load();

            RateCard rateCard = new RateCard();

            return View(rateCard);
        }

        [HttpPost]
        public ActionResult Create(RateCard rateCard)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                Load();

                return View(rateCard);
            }

            if (rateCardService.Value.FindBy(x => x.CategoryId == rateCard.CategoryId
                                          && x.ClothId == rateCard.ClothId
                                          && x.ServiceTypeId == rateCard.ServiceTypeId)
                                              .FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                Load();

                return View(rateCard);
            }

            rateCard.ClothId = rateCard.DDCloth;
            rateCard.CreatedBy = UserName;

            rateCardService.Value.Add(rateCard);
            rateCardService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (rateCard.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = rateCardService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            Load();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RateCard rateCard, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                Load();

                return View(rateCard);
            }

            var oldRateCard = rateCardService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldRateCard.CategoryId != rateCard.CategoryId && oldRateCard.ClothId != rateCard.ClothId && oldRateCard.ServiceTypeId != rateCard.ServiceTypeId) &&
                rateCardService.Value.GetAll().Any(x => x.CategoryId == rateCard.CategoryId && x.ClothId == rateCard.ClothId && x.ServiceTypeId == rateCard.ServiceTypeId))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                Load();

                return View(rateCard);
            }

            oldRateCard.CategoryId = rateCard.CategoryId;
            oldRateCard.ServiceTypeId = rateCard.ServiceTypeId;
            oldRateCard.ClothId = rateCard.DDCloth;
            oldRateCard.UrgetRate = rateCard.UrgetRate;
            oldRateCard.NormalRate = rateCard.NormalRate;
            oldRateCard.Status = rateCard.Status;
            oldRateCard.UpdatedBy = UserName;
            oldRateCard.UpdatedDate = DateTime.Now;

            rateCardService.Value.Edit(oldRateCard);
            rateCardService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = rateCardService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(RateCard rateCard, int id)
        {
            var oldRateCard = rateCardService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            rateCardService.Value.Delete(oldRateCard);
            rateCardService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public JsonResult DDClothByCategoryId(int id)
        {
            return Json(this.clothsService.Value.FindBy(x => x.CategoryId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        private void Load()
        {
            ViewBag.Categories = categoryService.Value.GetAll()
                                .Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() })
                                .ToList();

            ViewBag.ServiceType = serviceTypeService.Value.GetAll()
                               .Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() })
                               .ToList();
        }
    }
}