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
    //[OutputCache(NoPackage = true, Duration = 0)]
    public class PackageController : BaseController
    {
        private readonly IPackageService packageService;
        private readonly IOrderService orderService;

        public PackageController(IPackageService packageService,
            IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger,
            IOrderService orderService) : base(userMasterService, logger)
        {
            this.packageService = packageService;
            this.orderService = orderService;
        }

        // GET: Package
        public ActionResult Index()
        {
            return View(packageService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Package package)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(package);
            }

            if (packageService.FindBy(x => x.Name == package.Name &&
                                         x.Diemension == package.Diemension).FirstOrDefault() != null)
            {
                TempData["warnig-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(package);
            }

            package.CreatedBy = UserName;

            packageService.Add(package);
            packageService.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (package.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var package = packageService.FindBy(x => x.ID == id).FirstOrDefault();

            return View(package);
        }

        [HttpPost]
        public ActionResult Edit(int id, Package package)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(package);
            }

            var oldPackage = packageService.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldPackage.Name != package.Name && oldPackage.Diemension != package.Diemension) &&
                 packageService.GetAll().Any(x => x.Name == package.Name && x.Diemension == package.Diemension))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(package);
            }

            oldPackage.Name = package.Name;
            oldPackage.Diemension = package.Diemension;
            oldPackage.Description = package.Description;
            oldPackage.Rate = package.Rate;
            oldPackage.Heading = package.Heading;
            oldPackage.BagCount = package.BagCount;
            oldPackage.Status = package.Status;
            oldPackage.UpdatedBy = UserName;
            oldPackage.UpdatedDate = DateTime.Now;

            packageService.Edit(oldPackage);
            packageService.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var package = packageService.FindBy(x => x.ID == id).FirstOrDefault();

            package.CanDelete = CanDelete(id);

            return View(package);
        }

        [HttpPost]
        public ActionResult Delete(Package package, int id)
        {
            var oldPackage = packageService.FindBy(x => x.ID == id).FirstOrDefault();

            packageService.Delete(oldPackage);
            packageService.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        private bool CanDelete(int id)
        {
            var result = orderService.FindBy(x => x.PackageId == id).Count() > 0;
            return result;
        }
    }
}