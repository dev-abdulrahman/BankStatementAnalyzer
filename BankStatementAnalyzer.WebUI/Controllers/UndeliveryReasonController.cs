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
    public class UndeliveryReasonController : BaseController
    {
        private readonly Lazy<IUndeliveryReasonService> undeliveryReasonService;

        public UndeliveryReasonController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<IUndeliveryReasonService> undeliveryReasonService) : base(userMasterService.Value, logger.Value)
        {
            this.undeliveryReasonService = undeliveryReasonService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            
            return View(undeliveryReasonService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            UndeliveryReason undeliveryReason = new UndeliveryReason();

            return View(undeliveryReason);
        }

        [HttpPost]
        public ActionResult Create(UndeliveryReason undeliveryReason)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(undeliveryReason);
            }

            if (undeliveryReasonService.Value.FindBy(x => x.Reason == undeliveryReason.Reason).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(undeliveryReason);
            }

            undeliveryReason.CreatedBy = UserName;

            undeliveryReasonService.Value.Add(undeliveryReason);
            undeliveryReasonService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (undeliveryReason.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = undeliveryReasonService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UndeliveryReason undeliveryReason, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(undeliveryReason);
            }

            var oldReason = undeliveryReasonService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldReason.Reason != undeliveryReason.Reason) && undeliveryReasonService.Value.GetAll().Any(x => x.Reason == undeliveryReason.Reason))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(undeliveryReason);
            }

            oldReason.Reason = undeliveryReason.Reason;
            oldReason.Status = undeliveryReason.Status;
            oldReason.UpdatedBy = UserName;
            oldReason.UpdatedDate = DateTime.Now;

            undeliveryReasonService.Value.Edit(oldReason);
            undeliveryReasonService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = undeliveryReasonService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(UndeliveryReason undeliveryReason, int id)
        {
            var oldUndeliveryReason = undeliveryReasonService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            undeliveryReasonService.Value.Delete(oldUndeliveryReason);
            undeliveryReasonService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}