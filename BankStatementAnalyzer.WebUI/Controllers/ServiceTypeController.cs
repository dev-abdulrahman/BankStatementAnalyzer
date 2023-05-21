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
    public class ServiceTypeController : BaseController
    {
        private readonly Lazy<IServiceTypeService> serviceTypeService;

        public ServiceTypeController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<IServiceTypeService> serviceTypeService) : base(userMasterService.Value, logger.Value)
        {
            this.serviceTypeService = serviceTypeService;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View(serviceTypeService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ServiceType serviceType = new ServiceType();

            return View(serviceType);
        }

        [HttpPost]
        public ActionResult Create(ServiceType serviceType, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(serviceType);
            }

            if (serviceTypeService.Value.FindBy(x => x.Name == serviceType.Name).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(serviceType);
            }

            serviceType.CreatedBy = UserName;

            serviceTypeService.Value.Add(serviceType);
            serviceTypeService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (serviceType.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = serviceTypeService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ServiceType serviceType, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(serviceType);
            }

            var oldServiceType = serviceTypeService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldServiceType.Name != serviceType.Name) &&
                serviceTypeService.Value.GetAll().Any(x => x.Name == serviceType.Name))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(serviceType);
            }

            oldServiceType.Name = serviceType.Name;
            oldServiceType.Status = serviceType.Status;
            oldServiceType.UpdatedBy = UserName;
            oldServiceType.UpdatedDate = DateTime.Now;

            serviceTypeService.Value.Edit(oldServiceType);
            serviceTypeService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = serviceTypeService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ServiceType serviceType, int id)
        {
            var oldServiceType = serviceTypeService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            serviceTypeService.Value.Delete(oldServiceType);
            serviceTypeService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}