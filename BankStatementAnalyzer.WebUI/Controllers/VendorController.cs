using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class VendorController : BaseController
    {
        private readonly IVendorService vendorService;
        public VendorController(IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger,
            IVendorService vendorService) : base(userMasterService, logger)
        {
            this.vendorService = vendorService;
        }

        // GET: Vendor
        public ActionResult Index()
        {
            return View(vendorService.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Vendor vendor = new Vendor
            {
                Status = true
            };
            return View(vendor);
        }

        [HttpPost]
        public ActionResult Create(Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View();
            }

            if (vendorService.FindBy(x => x.Name == vendor.Name && x.Mobile == vendor.Mobile).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(vendor);
            }

            vendor.CreatedBy = UserName;

            vendorService.Add(vendor);
            vendorService.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (vendor.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var vendor = vendorService.FindBy(x => x.ID == id).FirstOrDefault();
            return View(vendor);
        }

        [HttpPost]
        public ActionResult Edit(Vendor vendor, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(vendor);
            }

            var oldVendor = vendorService.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldVendor.Name != vendor.Name && oldVendor.Mobile != vendor.Mobile) &&
                 vendorService.GetAll().Any(x => x.Name == vendor.Name && x.Mobile == vendor.Mobile))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(vendor);
            }

            oldVendor.UpdatedBy = UserName;
            oldVendor.Name = vendor.Name;
            oldVendor.Email = vendor.Email;
            oldVendor.Address = vendor.Address;
            oldVendor.Status = vendor.Status;
            oldVendor.Mobile = vendor.Mobile;
            oldVendor.Latitude = vendor.Latitude;
            oldVendor.Longitude = vendor.Longitude;

            vendorService.Edit(oldVendor);
            vendorService.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}