using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [AllowAnonymous]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class PageManagerController : BaseController
    {
        private readonly Lazy<IPageManagerService> pageManagerService;

        public PageManagerController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<IPageManagerService> pageManagerService) : base(userMasterService.Value, logger.Value)
        {
            this.pageManagerService = pageManagerService;
        }

        // GET: Email
        public ActionResult Index()
        {
            
            return View(pageManagerService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            PageManager pageManager = new PageManager();

            return View(pageManager);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PageManager pageManager)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(pageManager);
            }

            if (pageManagerService.Value.FindBy(x => x.PageName == pageManager.PageName && x.PageTitle == pageManager.PageTitle).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(pageManager);
            }

            pageManager.CreatedBy = UserName;

            pageManagerService.Value.Add(pageManager);
            pageManagerService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (pageManager.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = pageManagerService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PageManager pageManager, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(pageManager);
            }

            var oldPageManager = pageManagerService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldPageManager.PageName != pageManager.PageName) &&
                pageManagerService.Value.GetAll().Any(x => x.PageName == pageManager.PageName))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(pageManager);
            }

            oldPageManager.PageName = pageManager.PageName;
            oldPageManager.PageTitle = pageManager.PageTitle;
            oldPageManager.PageMetaKeyword = pageManager.PageMetaKeyword;
            oldPageManager.PageMetaDescription = pageManager.PageMetaDescription;
            oldPageManager.PageDescription = pageManager.PageDescription;
            oldPageManager.Status = pageManager.Status;
            oldPageManager.UpdatedBy = UserName;
            oldPageManager.UpdatedDate = DateTime.Now;

            pageManagerService.Value.Edit(oldPageManager);
            pageManagerService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = pageManagerService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Email email, int id)
        {
            var oldPageManager = pageManagerService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            pageManagerService.Value.Delete(oldPageManager);
            pageManagerService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}