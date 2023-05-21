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
    public class StoreController : BaseController
    {
        private readonly IStoreService storeService;

        public StoreController(IStoreService storeService,
            IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger) : base(userMasterService, logger)
        {
            this.storeService = storeService;
        }

        // GET: Store
        public ActionResult Index()
        {
            
            return View(storeService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Store store)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(store);
            }

            

            if (storeService.FindBy(x => x.Name == store.Name &&
                                         x.Latitude == store.Latitude &&
                                         x.Longitude == store.Longitude).FirstOrDefault() != null)
            {
                TempData["warnig-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(store);
            }

            store.CreatedBy = UserName;

            storeService.Add(store);
            storeService.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (store.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var store = storeService.FindBy(x => x.ID == id).FirstOrDefault();

            return View(store);
        }

        [HttpPost]
        public ActionResult Edit(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(store);
            }

            

            var oldStore = storeService.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldStore.Name != store.Name) &&
                 storeService.GetAll().Any(x => x.Name == store.Name))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(store);
            }

            oldStore.Name = store.Name;
            oldStore.Address = store.Address;
            oldStore.PhoneNumber = store.PhoneNumber;
            oldStore.StoreType = store.StoreType;
            oldStore.Pincode = store.Pincode;
            oldStore.Latitude = store.Latitude;
            oldStore.Longitude = store.Longitude;
            oldStore.Status = store.Status;
            oldStore.UpdatedBy = UserName;
            oldStore.IsInstaServiceProvided = store.IsInstaServiceProvided;
            oldStore.UpdatedDate = DateTime.Now;

            storeService.Edit(oldStore);
            storeService.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var store = storeService.FindBy(x => x.ID == id).FirstOrDefault();

            return View(store);
        }

        [HttpPost]
        public ActionResult Delete(Store store, int id)
        {
            var oldStore = storeService.FindBy(x => x.ID == id).FirstOrDefault();

            storeService.Delete(oldStore);
            storeService.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}