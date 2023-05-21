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
    public class SupportAndFAQController : BaseController
    {
        private readonly Lazy<ISupportAndFAQService> supportAndFAQService;

        public SupportAndFAQController(Lazy<IUserMasterService> userMasterService,
            Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<ISupportAndFAQService> supportAndFAQService) : base(userMasterService.Value, logger.Value)
        {
            this.supportAndFAQService = supportAndFAQService;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View(supportAndFAQService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            SupportAndFAQ support = new SupportAndFAQ();

            return View(support);
        }

        [HttpPost]
        public ActionResult Create(SupportAndFAQ support)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(support);
            }

            if (supportAndFAQService.Value.FindBy(x => x.Title == support.Title).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(support);
            }

            support.FAQType = FAQType.Customer;
            support.CreatedBy = UserName;

            supportAndFAQService.Value.Add(support);
            supportAndFAQService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (support.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = supportAndFAQService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SupportAndFAQ support, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(support);
            }

            var oldFAQ = supportAndFAQService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if (oldFAQ.Title != support.Title && supportAndFAQService.Value.GetAll().Any(x => x.Title == support.Title))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(support);
            }

            oldFAQ.FAQType = FAQType.Customer;
            oldFAQ.Title = support.Title;
            oldFAQ.Description = support.Description;
            oldFAQ.Status = support.Status;
            oldFAQ.UpdatedBy = UserName;
            oldFAQ.UpdatedDate = DateTime.Now;

            supportAndFAQService.Value.Edit(oldFAQ);
            supportAndFAQService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = supportAndFAQService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(SupportAndFAQ support, int id)
        {
            var oldFAQ = supportAndFAQService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            supportAndFAQService.Value.Delete(oldFAQ);
            supportAndFAQService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}