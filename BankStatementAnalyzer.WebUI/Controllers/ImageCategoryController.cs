using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class ImageCategoryController : BaseController
    {
        private readonly Lazy<IImageCategoryService> imageCategoryService;

        public ImageCategoryController(Lazy<ILoggerFacade<BaseController>> logger,
           Lazy<IUserMasterService> userMasterService,
           Lazy<IImageCategoryService> imageCategoryService) : base(userMasterService.Value, logger.Value)
        {
            this.imageCategoryService = imageCategoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            
            return View(imageCategoryService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ImageCategory imageCategory = new ImageCategory();

            return View(imageCategory);
        }

        [HttpPost]
        public ActionResult Create(ImageCategory imageCategory)
        {
            
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(imageCategory);
            }

            if (imageCategoryService.Value.FindBy(x => x.Name == imageCategory.Name).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(imageCategory);
            }

            imageCategory.CreatedBy = UserName;

            imageCategoryService.Value.Add(imageCategory);
            imageCategoryService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (imageCategory.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            var model = imageCategoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ImageCategory imageCategory, int id)
        {
            
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(imageCategory);
            }

            var oldImage = imageCategoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldImage.Name != imageCategory.Name) &&
                imageCategoryService.Value.GetAll().Any(x => x.Name == imageCategory.Name))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(imageCategory);
            }

            oldImage.Name = imageCategory.Name;
            oldImage.Status = imageCategory.Status;
            oldImage.UpdatedBy = UserName;
            oldImage.UpdatedDate = DateTime.Now;

            imageCategoryService.Value.Edit(oldImage);
            imageCategoryService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            
            var model = imageCategoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(City city, int id)
        {
            
            var oldImage = imageCategoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            imageCategoryService.Value.Delete(oldImage);
            imageCategoryService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}