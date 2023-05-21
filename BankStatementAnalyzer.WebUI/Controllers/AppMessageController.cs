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
    public class AppMessageController : BaseController
    {
        private readonly IAppMessageService appMessageService;
        public AppMessageController(IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger,
            IAppMessageService appMessageService) : base(userMasterService, logger)
        {
            this.appMessageService = appMessageService;
        }

        // GET: AppMessage
        public ActionResult Index()
        {
            return View(appMessageService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AppMessage appMessage)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(appMessage);
            }

            appMessage.CreatedBy = UserName;

            appMessageService.Add(appMessage);
            appMessageService.Save();

            if (appMessage.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(appMessageService.FindBy(x => x.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, AppMessage appMessage)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(appMessage);
            }

            var oldAppmessage = appMessageService.FindBy(x => x.ID == id).FirstOrDefault();
            

            if ((oldAppmessage.Message != appMessage.Message && oldAppmessage.Key != appMessage.Key) &&
                appMessageService.GetAll().Any(x => x.Message == appMessage.Message && x.Key == appMessage.Key))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(appMessage);
            }

            oldAppmessage.Message = appMessage.Message;
            oldAppmessage.Key = appMessage.Key;
            oldAppmessage.Status = appMessage.Status;
            oldAppmessage.UpdatedBy = UserName;
            oldAppmessage.UpdatedDate = DateTime.Now;

            appMessageService.Edit(oldAppmessage);
            appMessageService.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}