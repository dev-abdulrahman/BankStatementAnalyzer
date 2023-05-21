using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class AreaManagerController : BaseController
    {
        private readonly Lazy<IAreaManagerService> areaManagerService;
        private readonly Lazy<ICityService> cityService;

        public AreaManagerController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<IAreaManagerService> areaManagerService,
            Lazy<ICityService> cityService) : base(userMasterService.Value, logger.Value)
        {
            this.areaManagerService = areaManagerService;
            this.cityService = cityService;
        }

        // GET: AreaManager
        public ActionResult Index()
        {
            
            return View(areaManagerService.Value.GetAll().Include("City").ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Load();
            AreaManager areaManager = new AreaManager();

            return View(areaManager);
        }

        [HttpPost]
        public ActionResult Create(AreaManager areaManager)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;
                Load();
                return View(areaManager);
            }

            if (areaManagerService.Value.FindBy(x => x.Name == areaManager.Name && x.Pincode == areaManager.Pincode).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;
                Load();
                return View(areaManager);
            }

            areaManager.CreatedBy = UserName;

            areaManagerService.Value.Add(areaManager);
            areaManagerService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (areaManager.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Load();

            var model = areaManagerService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(AreaManager areaManager, int id)
        {
            if (!ModelState.IsValid)
            {
                Load();

                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(areaManager);
            }

            var oldArea = areaManagerService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldArea.Name != areaManager.Name && oldArea.Pincode != areaManager.Pincode) &&
                areaManagerService.Value.GetAll().Any(x => x.Name == areaManager.Name && x.Pincode == areaManager.Pincode))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(areaManager);
            }

            oldArea.Name = areaManager.Name;
            oldArea.Pincode = areaManager.Pincode;
            oldArea.CityId = areaManager.CityId;
            oldArea.Status = areaManager.Status;
            oldArea.UpdatedBy = UserName;
            oldArea.UpdatedDate = DateTime.Now;

            areaManagerService.Value.Edit(oldArea);
            areaManagerService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = areaManagerService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(City city, int id)
        {
            var oldArea = areaManagerService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            areaManagerService.Value.Delete(oldArea);
            areaManagerService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        private void Load()
        {
            ViewBag.Cities = cityService.Value
                            .GetAll()
                            .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                            .ToList();
        }
    }
}