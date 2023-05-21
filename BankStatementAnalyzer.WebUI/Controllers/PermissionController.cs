using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class PermissionController : BaseController
    {
        private readonly IActionService actionService;
        private readonly IPermissionMasterService permissionMasterService;
        private readonly IUserMasterService userMasterService;
        private readonly ILoggerFacade<BaseController> logger;

        public PermissionController(IActionService actionService,
           IPermissionMasterService permissionMasterService,
          IUserMasterService userMasterService,
          ILoggerFacade<BaseController> logger) : base(userMasterService, logger)
        {
            this.actionService = actionService;
            this.permissionMasterService = permissionMasterService;
            this.userMasterService = userMasterService;
        }

        // GET: Permission
        public ActionResult Index()
        {
            return View(permissionMasterService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewModels.Permission.PermissionVModel model = new ViewModels.Permission.PermissionVModel();
            model.Actions = ViewModels.Permission.ActionVM.ConvertAll(actionService.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ViewModels.Permission.PermissionVModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (permissionMasterService.FindBy(x => x.Name == model.PermissionInfo.Name && x.ID == model.PermissionInfo.ID).FirstOrDefault() == null)
                    {
                        List<ViewModels.Permission.ActionVM> permissionActions = model.Actions.Where(p => p.IsSelected).ToList();

                        foreach (ViewModels.Permission.ActionVM actions in permissionActions)
                        {
                            BankStatementAnalyzer.Models.Action modelAction = actionService.FindBy(x => x.ID == actions.ActionInfo.ID).SingleOrDefault();
                            model.PermissionInfo.Actions.Add(modelAction);
                        }

                        model.PermissionInfo.CreatedOn = DateTime.Now;
                        model.PermissionInfo.CreatedBy = UserName;

                        permissionMasterService.Add(model.PermissionInfo);
                        permissionMasterService.Save();

                        TempData["success-message"] = "Permission added successfully.";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("RoleInfo.Name", "Role Name already exists.");

                        model.Actions = ViewModels.Permission.ActionVM.ConvertAll(actionService.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                        return View(model);
                    }
                }
                else
                {
                    model.Actions = ViewModels.Permission.ActionVM.ConvertAll(actionService.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                model.Actions = ViewModels.Permission.ActionVM.ConvertAll(actionService.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Permission permission = permissionMasterService.FindBy(x => x.ID == id).SingleOrDefault();
            ViewModels.Permission.PermissionVModel model = new ViewModels.Permission.PermissionVModel();
            model.PermissionInfo = permission;
            model.Actions = ViewModels.Permission.ActionVM.ConvertAll(actionService.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());
            foreach (BankStatementAnalyzer.Models.Action action in permission.Actions)
            {
                model.Actions.Find(p => p.ActionInfo.Name == action.Name).IsSelected = true;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, ViewModels.Permission.PermissionVModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (permissionMasterService.FindBy(x => x.Name == model.PermissionInfo.Name && x.ID == model.PermissionInfo.ID).FirstOrDefault() == null)
                    {
                        Permission dbPermission = permissionMasterService.FindBy(x => x.ID == id).SingleOrDefault();
                        dbPermission.Name = model.PermissionInfo.Name;
                        dbPermission.Feature = model.PermissionInfo.Feature;

                        dbPermission.Actions.Clear();
                        List<ViewModels.Permission.ActionVM> permissionActions = model.Actions.Where(p => p.IsSelected).ToList();
                        foreach (ViewModels.Permission.ActionVM action in permissionActions)
                        {
                            BankStatementAnalyzer.Models.Action modelAction = actionService.FindBy(x => x.ID == action.ActionInfo.ID).SingleOrDefault();
                            dbPermission.Actions.Add(modelAction);
                        }

                        dbPermission.ModifiedOn = DateTime.Now;
                        dbPermission.ModifiedBy = UserName;
                        permissionMasterService.Edit(dbPermission);

                        permissionMasterService.Save();

                        TempData["success-message"] = "Permission edited successfully.";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("RoleInfo.Name", "Role Name already exists.");
                        model.Actions = ViewModels.Permission.ActionVM.ConvertAll(actionService.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                        return View(model);
                    }
                }
                else
                {
                    model.Actions = ViewModels.Permission.ActionVM.ConvertAll(actionService.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                model.Actions = ViewModels.Permission.ActionVM.ConvertAll(actionService.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            Permission permission = permissionMasterService.FindBy(x => x.ID == id).SingleOrDefault();

            return View(permission);
        }

        //
        // POST: /Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Permission model)
        {
            try
            {
                Permission permission = permissionMasterService.FindBy(x => x.ID == id).SingleOrDefault();

                permissionMasterService.Delete(model);
                permissionMasterService.Save();

                TempData["success-message"] = "Permission deleted successfully.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}