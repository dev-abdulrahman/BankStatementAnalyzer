using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.SuperAdmin.Common.constants;
using BankStatementAnalyzer.SuperAdmin.Common.logger;
using BankStatementAnalyzer.SuperAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.SuperAdmin.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class RoleMasterController : BaseController
    {
        private readonly Lazy<IRoleMasterService> roleMasterService;
        private readonly Lazy<IPermissionMasterService> permissionMasterService;
        private readonly Lazy<ILoggerFacade<RoleMasterController>> logger;
        private readonly Lazy<IUserMasterService> userMasterService;

        public RoleMasterController(Lazy<IRoleMasterService> roleMasterService,
            Lazy<IPermissionMasterService> permissionMasterService,
            Lazy<IUserMasterService> userMasterService,
            Lazy<ILoggerFacade<RoleMasterController>> logger) : base(userMasterService.Value)
        {
            this.roleMasterService = roleMasterService;
            this.permissionMasterService = permissionMasterService;
            this.logger = logger;
            this.userMasterService = userMasterService;
        }

        // GET: RoleMaster
        public ActionResult Index()
        {
            var roles = roleMasterService.Value.GetAll().ToList();

            return View(roles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewModels.Roles.RoleVM model = new ViewModels.Roles.RoleVM();

            model.RoleInfo.DefaultController = "Home";
            model.RoleInfo.DefaultAction = "Index";

            model.Permissions = ViewModels.Roles.PermissionVM.ConvertAll(permissionMasterService.Value.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());
            model.Permissions.Find(p => p.PermissionInfo.Name == "View home").IsSelected = true;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ViewModels.Roles.RoleVM role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (roleMasterService.Value.GetRoleByName(role.RoleInfo.Name, role.RoleInfo.ID) == null)
                    {
                        List<ViewModels.Roles.PermissionVM> rolePermissions = role.Permissions.Where(p => p.IsSelected).ToList();

                        foreach (ViewModels.Roles.PermissionVM permission in rolePermissions)
                        {
                            Permission modelPermission = permissionMasterService.Value.FindBy(x => x.ID == permission.PermissionInfo.ID).SingleOrDefault();
                            role.RoleInfo.Permissions.Add(modelPermission);
                        }

                        role.RoleInfo.CreatedOn = DateTime.Now;
                        role.RoleInfo.CreatedBy = UserName;
                        roleMasterService.Value.Add(role.RoleInfo);
                        roleMasterService.Value.Save();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("RoleInfo.Name", "Role Name already exists.");
                        role.Permissions = ViewModels.Roles.PermissionVM.ConvertAll(permissionMasterService.Value.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                        return View(role);
                    }
                }
                else
                {
                    role.Permissions = ViewModels.Roles.PermissionVM.ConvertAll(permissionMasterService.Value.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                    return View(role);
                }
            }
            catch (Exception ex)
            {
                role.Permissions = ViewModels.Roles.PermissionVM.ConvertAll(permissionMasterService.Value.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                logger.Value.Error(ex.Message, ex);

                return View(role);
            }
        }

        public ActionResult Edit(int id)
        {
            Role role = roleMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();
            ViewModels.Roles.RoleVM model = new ViewModels.Roles.RoleVM();
            model.RoleInfo = role;
            model.Permissions = ViewModels.Roles.PermissionVM.ConvertAll(permissionMasterService.Value.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());
            foreach (Permission permission in role.Permissions)
            {
                model.Permissions.Find(p => p.PermissionInfo.Name == permission.Name).IsSelected = true;
            }
            return View(model);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ViewModels.Roles.RoleVM role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (roleMasterService.Value.GetRoleByName(role.RoleInfo.Name, id) == null)
                    {
                        Role dbRole = roleMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();
                        dbRole.Name = role.RoleInfo.Name;
                        dbRole.DefaultController = role.RoleInfo.DefaultController;
                        dbRole.DefaultAction = role.RoleInfo.DefaultAction;

                        dbRole.Permissions.Clear();
                        List<ViewModels.Roles.PermissionVM> rolePermissions = role.Permissions.Where(p => p.IsSelected).ToList();
                        foreach (ViewModels.Roles.PermissionVM permission in rolePermissions)
                        {
                            Permission modelPermission = permissionMasterService.Value.FindBy(x => x.ID == permission.PermissionInfo.ID).SingleOrDefault();
                            dbRole.Permissions.Add(modelPermission);
                        }

                        dbRole.ModifiedOn = DateTime.Now;
                        dbRole.ModifiedBy = UserName;
                        roleMasterService.Value.Edit(dbRole);

                        roleMasterService.Value.Save();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("RoleInfo.Name", "Role Name already exists.");
                        role.Permissions = ViewModels.Roles.PermissionVM.ConvertAll(permissionMasterService.Value.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                        return View(role);
                    }
                }
                else
                {
                    role.Permissions = ViewModels.Roles.PermissionVM.ConvertAll(permissionMasterService.Value.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                    return View(role);
                }
            }
            catch (Exception ex)
            {
                role.Permissions = ViewModels.Roles.PermissionVM.ConvertAll(permissionMasterService.Value.GetAll().OrderBy(r => r.Feature).ThenBy(r => r.Name).ToList());

                logger.Value.Error(ex.Message, ex);

                return View(role);
            }
        }

        public ActionResult Delete(int id)
        {
            Role role = roleMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();

            return View(role);
        }

        //
        // POST: /Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Role role)
        {
            try
            {
                var model = roleMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();

                var user = userMasterService.Value.GetAll().Include("Roles").ToList();

                if (user.Any(x => x.Roles.Any(z => z.Name == model.Name)))
                {
                    TempData["error-message"] = ConstantValue.MASTER_DELETE_ERROR_MESSAGE;

                    return RedirectToAction("Index");
                }

                model.Permissions.Clear();
                roleMasterService.Value.Delete(model);
                roleMasterService.Value.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Value.Error(ex.Message, ex);

                TempData["error-message"] = ConstantValue.MASTER_DELETE_ERROR_MESSAGE;

                return RedirectToAction("Index");
            }
        }
    }
}