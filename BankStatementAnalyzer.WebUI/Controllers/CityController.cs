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
    public class CityController : BaseController
    {
        private readonly Lazy<ICityService> cityService;

        public CityController(Lazy<ILoggerFacade<BaseController>> logger,
            Lazy<IUserMasterService> userMasterService,
            Lazy<ICityService> cityService) : base(userMasterService.Value, logger.Value)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            
            return View(cityService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            City city = new City();

            return View(city);
        }

        [HttpPost]
        public ActionResult Create(City city)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(city);
            }

            if (cityService.Value.FindBy(x => x.Name == city.Name && x.Code == city.Code).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(city);
            }

            city.CreatedBy = UserName;

            cityService.Value.Add(city);
            cityService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (city.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = cityService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(City city, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(city);
            }

            var oldCity = cityService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if ((oldCity.Name != city.Name && oldCity.Code != city.Code) && cityService.Value.GetAll().Any(x => x.Name == city.Name && x.Code == city.Code))
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(city);
            }

            oldCity.Name = city.Name;
            oldCity.Code = city.Code;
            oldCity.Status = city.Status;
            oldCity.UpdatedBy = UserName;
            oldCity.UpdatedDate = DateTime.Now;

            cityService.Value.Edit(oldCity);
            cityService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = cityService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(City city, int id)
        {
            var oldCity = cityService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            cityService.Value.Delete(oldCity);
            cityService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }
    }
}