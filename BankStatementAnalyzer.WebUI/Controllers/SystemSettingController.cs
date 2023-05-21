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
    public class SystemSettingController : Controller
    {
        private readonly Lazy<ISystemSettingService> systemSettingService;
        private readonly Lazy<ILoggerFacade<SystemSetting>> logger;


        public SystemSettingController(Lazy<ISystemSettingService> systemSettingService, Lazy<ILoggerFacade<SystemSetting>> logger)
        {
            this.systemSettingService = systemSettingService;
            this.logger = logger;
        }
        public ActionResult Index()
        {
            var data = systemSettingService.Value.GetAll().Where(x => x.IsVisibleToAdmin);
            return View(data);
        }


        public ActionResult Details(int id)
        {
            var details = systemSettingService.Value.FindBy(x => x.ID == id).SingleOrDefault();

            return View(details);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SystemSetting model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (systemSettingService.Value.IsExist(model.SettingTypeValue))
                    {
                        ViewBag.IsExist = "Name already exist.";
                        return View("create", model);
                    }
                    systemSettingService.Value.Add(model);
                    systemSettingService.Value.Save();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.Value.Error(ex.Message, ex);
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {

            var editdata = systemSettingService.Value.FindBy(x => x.ID == id).SingleOrDefault();
            TempData["GetData"] = editdata;
            return View(editdata);
        }


        [HttpPost, ValidateInput(false)]
        [ActionName("Edit")]
        public ActionResult EditPost(SystemSetting model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var modeldata = TempData["GetData"] as SystemSetting;
                    TempData.Keep("GetData");

                    if (systemSettingService.Value.IsDuplicate(model.SettingTypeValue, modeldata.SettingTypeValue))
                    {
                        ViewBag.IsExist = "Name already exist.";
                        return View("Edit", model);
                    }

                    systemSettingService.Value.Edit(model);
                    systemSettingService.Value.Save();

                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                logger.Value.Error(ex.Message, ex);
                return View(model);
            }

        }

        public ActionResult Delete(int id)
        {
            var model = systemSettingService.Value.FindBy(x => x.ID == id).SingleOrDefault();

            return View(model);

        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {

            try
            {
                var Model = systemSettingService.Value.FindBy(x => x.ID == id).SingleOrDefault();
                systemSettingService.Value.Delete(Model);
                systemSettingService.Value.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Value.Error(ex.Message, ex);
                return View();
            }
        }
    }
}