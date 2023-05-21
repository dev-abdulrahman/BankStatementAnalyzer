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
    public class StreetController : BaseController
    {
        private readonly Lazy<IStreetService> streetService;
        private readonly Lazy<IAreaManagerService> areaManagerService;

        public StreetController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<IStreetService> streetService,
            Lazy<IAreaManagerService> areaManagerService) : base(userMasterService.Value, logger.Value)
        {
            this.streetService = streetService;
            this.areaManagerService = areaManagerService;
        }

        // GET: Street
        public ActionResult Index()
        {
            
            return View(streetService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Load();

            Street street = new Street();

            return View(street);
        }

        [HttpPost]
        public ActionResult Create(Street street)
        {
            if (!ModelState.IsValid)
            {
                Load();

                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(street);
            }

            if (streetService.Value.FindBy(x => x.Name == street.Name).FirstOrDefault() != null)
            {
                Load();

                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(street);
            }

            street.CreatedBy = UserName;

            streetService.Value.Add(street);
            streetService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (street.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Load();

            var model = streetService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Street street, int id)
        {
            if (!ModelState.IsValid)
            {
                Load();

                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(street);
            }

            var oldStreet = streetService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldStreet.Name != street.Name && oldStreet.AreaId != street.AreaId) &&
                 streetService.Value.GetAll().Any(x => x.Name == street.Name && x.AreaId == street.AreaId))
            {
                Load();

                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(street);
            }

            oldStreet.Name = street.Name;
            oldStreet.AreaId = street.AreaId;
            oldStreet.Status = street.Status;
            oldStreet.UpdatedBy = UserName;
            oldStreet.UpdatedDate = DateTime.Now;

            streetService.Value.Edit(oldStreet);
            streetService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = streetService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Street street, int id)
        {
            var oldStreet = streetService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            streetService.Value.Delete(oldStreet);
            streetService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        public void Load()
        {
            ViewBag.Areas = areaManagerService.Value
                           .GetAll()
                           .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                           .ToList();
        }
    }
}