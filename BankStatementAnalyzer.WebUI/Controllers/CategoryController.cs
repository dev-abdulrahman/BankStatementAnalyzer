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
    public class CategoryController : BaseController
    {
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<IFileMasterService> fileMasterService;

        public CategoryController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<ICategoryService> categoryService,
            Lazy<IFileMasterService> fileMasterService) : base(userMasterService.Value, logger.Value)
        {
            this.categoryService = categoryService;
            this.fileMasterService = fileMasterService;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View(categoryService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Category category = new Category();

            return View(category);
        }

        [HttpPost]
        public ActionResult Create(Category category, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(category);
            }

            if (categoryService.Value.FindBy(x => x.Name == category.Name).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(category);
            }

            category.CreatedBy = UserName;

            categoryService.Value.Add(category);
            categoryService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (category.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = categoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Category category, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(category);
            }

            var oldCategory = categoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldCategory.Name != category.Name) &&
                categoryService.Value.GetAll().Any(x => x.Name == category.Name))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(category);
            }

            oldCategory.Name = category.Name;
            oldCategory.Status = category.Status;
            oldCategory.UpdatedBy = UserName;
            oldCategory.UpdatedDate = DateTime.Now;

            categoryService.Value.Edit(oldCategory);
            categoryService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = categoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Category category, int id)
        {
            var oldCategory = categoryService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            categoryService.Value.Delete(oldCategory);
            categoryService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}