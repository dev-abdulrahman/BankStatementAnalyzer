using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class UnitOfMeasureController : BaseController
    {
        private readonly IUserMasterService userMasterService;
        private readonly IUnitOfMeasureService unitOfMeasureService;

        public UnitOfMeasureController(IUserMasterService userMasterService,
            IUnitOfMeasureService unitOfMeasureService,
            ILoggerFacade<BaseController> logger) : base(userMasterService, logger)
        {
            this.userMasterService = userMasterService;
            this.unitOfMeasureService = unitOfMeasureService;
        }

        // GET: UnitOfMeasure
        public ActionResult Index()
        {
            
            return View(unitOfMeasureService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UnitOfMeasure unitOfMeasure)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = "Please input valid data.";

                return View(unitOfMeasure);
            }
            
            if (unitOfMeasureService.FindBy(x => x.Name == unitOfMeasure.Name).FirstOrDefault() != null)
            {
                TempData["warning-message"] = "Unit of measure with same name already exist.";

                return View(unitOfMeasure);
            }

            unitOfMeasure.CreatedBy = UserName;
            unitOfMeasureService.Add(unitOfMeasure);
            unitOfMeasureService.Save();

            TempData["success-message"] = "Unit of measure added successfully.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            return View(unitOfMeasureService.FindBy(x => x.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, UnitOfMeasure unitOfMeasure)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = "Please input valid data.";

                return View(unitOfMeasure);
            }

            unitOfMeasure.UpdatedBy = UserName;
            unitOfMeasure.UpdatedDate = DateTime.Now;

            unitOfMeasureService.Edit(unitOfMeasure);
            unitOfMeasureService.Save();

            TempData["success-message"] = "Unit of measure edited successfully.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            
            return View(unitOfMeasureService.FindBy(x => x.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, UnitOfMeasure unitOfMeasure)
        {
            
            var model = unitOfMeasureService.FindBy(x => x.ID == id).FirstOrDefault();

            unitOfMeasureService.Delete(model);
            unitOfMeasureService.Save();

            TempData["success-message"] = "Unit of measure deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}