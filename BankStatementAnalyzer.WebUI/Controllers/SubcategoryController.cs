using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using BankStatementAnalyzer.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class SubcategoryController : BaseController
    {
        private readonly Lazy<ISubCategoryService> subcategoryService;
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<IFileMasterService> fileMasterService;

        public SubcategoryController(Lazy<ISubCategoryService> subcategoryService,
            Lazy<ICategoryService> categoryService,
             ILoggerFacade<BaseController> logger,
             IUserMasterService userMasterService,
             Lazy<IFileMasterService> fileMasterService) : base(userMasterService, logger)
        {
            this.subcategoryService = subcategoryService;
            this.categoryService = categoryService;
            this.fileMasterService = fileMasterService;
        }

        // GET: Subcategory
        public ActionResult Index()
        {
            return View(GetCategories());
        }

        // GET: Subcategory/Details/5
        public ActionResult Details(int id)
        {
            
            var category = GetCategories().Where(x => x.SubCategory.ID == id).SingleOrDefault();

            return View(category);
        }

        // GET: Subcategory/Create
        public ActionResult Create()
        {


            ViewBag.CategoryList = categoryService.Value.GetAll();
            ViewBag.subCategoryList = subcategoryService.Value.GetAll();
            return View();
        }

        // POST: Subcategory/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SubCategory model, HttpPostedFileBase Files)
        {
            

            if (subcategoryService.Value.IsExist(model.Name, model.CategoryID))
            {
                ViewBag.IsExist = "Category with same name already exist";
                ViewBag.CategoryList = categoryService.Value.GetAll();
                return View("Create", model);
            }

            subcategoryService.Value.Add(model);
            subcategoryService.Value.Save();
            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;
            return RedirectToAction("Index");
        }

        // GET: Subcategory/Edit/5
        public ActionResult Edit(int id)
        {
            

            var subcategoryModel = subcategoryService.Value.FindBy(x => x.ID == id).SingleOrDefault();
            ViewBag.CategoryList = categoryService.Value.GetAll();
            ViewBag.subCategoryList = subcategoryService.Value.GetAll();

            return View(subcategoryModel);
        }

        // POST: Subcategory/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(SubCategory subcategory)
        {
            subcategory.UpdatedDate = DateTime.Now;
            subcategory.UpdatedBy = UserName;

            subcategoryService.Value.Edit(subcategory);
            subcategoryService.Value.Save();

            TempData["success-message"] = "Update Record successfully.";
            return RedirectToAction("Index");
        }

        // GET: Subcategory/Delete/5
        public ActionResult Delete(int id)
        {
            

            var subcategoryModel = subcategoryService.Value.FindBy(x => x.ID == id).SingleOrDefault();

            return View(subcategoryModel);
        }

        // POST: Subcategory/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            

            var subcategoryModel = subcategoryService.Value.FindBy(x => x.ID == id).SingleOrDefault();

            subcategoryService.Value.Delete(subcategoryModel);
            subcategoryService.Value.Save();

            TempData["success-message"] = "Delete Record successfully.";
            return RedirectToAction("Index");
        }

        private IEnumerable<CategorySubCategoryVM> GetCategories()
        {
            int cid = GetCompanyId();
            var categorySubCategoryModel = from cat in categoryService.Value.GetAll()
                                           join sub in subcategoryService.Value.GetAll() on cat.ID equals sub.CategoryID
                                           select new CategorySubCategoryVM()
                                           {
                                               Category = cat,
                                               SubCategory = sub
                                           };

            return categorySubCategoryModel.ToList();

        }
    }
}