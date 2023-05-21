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
    public class ClothsController : BaseController
    {
        private readonly Lazy<IClothsService> clothService;
        private readonly Lazy<ICategoryService> categoryService;

        public ClothsController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<IClothsService> clothService,
            Lazy<ICategoryService> categoryService) : base(userMasterService.Value, logger.Value)
        {
            this.clothService = clothService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(clothService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Load();

            Cloths cloth = new Cloths();

            return View(cloth);
        }

        [HttpPost]
        public ActionResult Create(Cloths cloth, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                Load();

                return View(cloth);
            }

            if (clothService.Value.FindBy(x => x.Name == cloth.Name).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                Load();

                return View(cloth);
            }

            cloth.CreatedBy = UserName;

            clothService.Value.Add(cloth);
            clothService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (cloth.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = clothService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            Load();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Cloths cloth, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                Load();

                return View(cloth);
            }

            var oldCloths = clothService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldCloths.Name != cloth.Name) &&
                clothService.Value.GetAll().Any(x => x.Name == cloth.Name))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                Load();

                return View(cloth);
            }

            oldCloths.CategoryId = cloth.CategoryId;
            oldCloths.Name = cloth.Name;
            oldCloths.Status = cloth.Status;
            oldCloths.UpdatedBy = UserName;
            oldCloths.UpdatedDate = DateTime.Now;

            clothService.Value.Edit(oldCloths);
            clothService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = clothService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Cloths cloth, int id)
        {
            var oldCloths = clothService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            clothService.Value.Delete(oldCloths);
            clothService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        private void Load()
        {
            ViewBag.Categories = categoryService.Value.GetAll()
                                .Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() })
                                .ToList();
        }
    }
}