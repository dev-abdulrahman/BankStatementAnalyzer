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
    public class EmailController : BaseController
    {
        private readonly Lazy<IEmailService> emailService;

        public EmailController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<IEmailService> emailService) : base(userMasterService.Value, logger.Value)
        {
            this.emailService = emailService;
        }

        // GET: Email
        public ActionResult Index()
        {
            
            return View(emailService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Email email = new Email();

            return View(email);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Email email)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(email);
            }

            if (emailService.Value.FindBy(x => x.EmailFor == email.EmailFor && x.EmailSubject == email.EmailSubject).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(email);
            }

            email.CreatedBy = UserName;

            emailService.Value.Add(email);
            emailService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (email.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = emailService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Email email, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(email);
            }

            var oldEmail = emailService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldEmail.EmailSubject != email.EmailSubject) &&
                emailService.Value.GetAll().Any(x => x.EmailSubject == email.EmailSubject))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(email);
            }

            oldEmail.EmailSubject = email.EmailSubject;
            oldEmail.EmailFor = email.EmailFor;
            oldEmail.Description = email.Description;
            oldEmail.Status = email.Status;
            oldEmail.UpdatedBy = UserName;
            oldEmail.UpdatedDate = DateTime.Now;

            emailService.Value.Edit(oldEmail);
            emailService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = emailService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Email email, int id)
        {
            var oldEmail = emailService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            emailService.Value.Delete(oldEmail);
            emailService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}