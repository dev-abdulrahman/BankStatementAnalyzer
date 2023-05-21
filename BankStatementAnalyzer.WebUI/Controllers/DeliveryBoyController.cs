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
    public class DeliveryBoyController : BaseController
    {
        private readonly IDeliveryBoyService deliveryBoyService;

        public DeliveryBoyController(IDeliveryBoyService deliveryBoyService,
            IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger) : base(userMasterService, logger)
        {
            this.deliveryBoyService = deliveryBoyService;
        }

        // GET: DeliveryBoy
        public ActionResult Index()
        {
            
            return View(deliveryBoyService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DeliveryBoy deliveryBoy)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(deliveryBoy);
            }

            if (deliveryBoyService.FindBy(x => x.Number == deliveryBoy.Number &&
                                          x.Name == deliveryBoy.Name && x.Email == deliveryBoy.Email &&
                                          x.VehicleNumber == deliveryBoy.VehicleNumber).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(deliveryBoy);
            }

            deliveryBoy.CreatedBy = UserName;

            deliveryBoyService.Add(deliveryBoy);
            deliveryBoyService.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == id).FirstOrDefault();
            return View(deliveryBoy);
        }

        [HttpPost]
        public ActionResult Edit(DeliveryBoy deliveryBoy, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(deliveryBoy);
            }

            var oldDelvieryBoy = deliveryBoyService.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldDelvieryBoy.Name != deliveryBoy.Name && oldDelvieryBoy.Email != deliveryBoy.Email) &&
                 deliveryBoyService.GetAll().Any(x => x.Name == deliveryBoy.Name && x.Email == deliveryBoy.Email
                 && x.VehicleNumber == deliveryBoy.VehicleNumber))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(deliveryBoy);
            }

            oldDelvieryBoy.UpdatedBy = UserName;
            oldDelvieryBoy.Name = deliveryBoy.Name;
            oldDelvieryBoy.Email = deliveryBoy.Email;
            oldDelvieryBoy.Address = deliveryBoy.Address;
            oldDelvieryBoy.Status = deliveryBoy.Status;
            oldDelvieryBoy.VehicleNumber = deliveryBoy.VehicleNumber;

            deliveryBoyService.Edit(oldDelvieryBoy);
            deliveryBoyService.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == id).FirstOrDefault();
            return View(deliveryBoy);
        }

        [HttpPost]
        public ActionResult Delete(int id, DeliveryBoy deliveryBoy)
        {
            var model = deliveryBoyService.FindBy(x => x.ID == id).FirstOrDefault();

            model.Status = false;
            model.UpdatedBy = UserName;
            model.UpdatedDate = DateTime.Now;

            deliveryBoyService.Edit(model);
            deliveryBoyService.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}