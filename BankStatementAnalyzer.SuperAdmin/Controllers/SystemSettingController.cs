using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.SuperAdmin.Common.logger;
using BankStatementAnalyzer.SuperAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.SuperAdmin.Controllers
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
            List<SystemSetting> data = new List<SystemSetting>();

            data = systemSettingService.Value.FindBy(x => x.IsVisibleToAdmin == true).ToList();
            return View(data);
        }

        public ActionResult Edit(int id, string access)
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

                    model.IsVisibleToAdmin = true;
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
    }
}