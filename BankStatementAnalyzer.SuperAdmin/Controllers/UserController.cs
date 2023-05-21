using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.SuperAdmin.App_Start;
using BankStatementAnalyzer.SuperAdmin.Common.constants;
using BankStatementAnalyzer.SuperAdmin.Filters;
using BankStatementAnalyzer.SuperAdmin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.SuperAdmin.Controllers
{
    [RouteAuthorize]
    ////[OutputCache(NoStore = true, Duration = 0)]
    public class UserController : BaseController
    {
        private ApplicationUserManager _userManager;
        private readonly Lazy<IUserMasterService> userMasterService;
        private readonly Lazy<IRoleMasterService> roleMasterService;
        private readonly Lazy<ICompanyService> companyService;
        private readonly Lazy<IUserCompanyMappingService> userCompanyMappingService;
        private readonly Lazy<ISystemSettingService> systemSettingService;

        public UserController(Lazy<IUserMasterService> userMasterService,
            Lazy<IRoleMasterService> roleMasterService,
            Lazy<ICompanyService> companyService,
            Lazy<IUserCompanyMappingService> userCompanyMappingService,
            Lazy<ISystemSettingService> systemSettingService) : base(userMasterService.Value)
        {
            this.userMasterService = userMasterService;
            this.roleMasterService = roleMasterService;
            this.companyService = companyService;
            this.userCompanyMappingService = userCompanyMappingService;
            this.systemSettingService = systemSettingService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var userList = userMasterService.Value.GetAll();

            return View(userList);
        }

        public ActionResult Create()
        {
            ViewModels.UserVM model = new ViewModels.UserVM();
            model.Roles = ViewModels.Roles.RoleVM.ConvertAll(roleMasterService.Value.GetAll().OrderBy(r => r.Name).ToList());
            Load();
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(ViewModels.UserVM user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user.Roles.Where(p => p.IsSelected).Count() == 0)
                    {
                        ModelState.AddModelError("", "One or more roles required.");
                    }
                    else
                    {
                        var appUser = new ApplicationUser { UserName = user.UserInfo.Email, Email = user.UserInfo.Email, PhoneNumber = user.Mobile };
                        User dbUser = new User();

                        var appDbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

                        using (var context = new BankStatementAnalyzerContext())
                        {
                            try
                            {
                                dbUser.Name = user.UserInfo.Name;

                                List<ViewModels.Roles.RoleVM> userRoles = user.Roles.Where(p => p.IsSelected).ToList();
                                foreach (ViewModels.Roles.RoleVM role in userRoles)
                                {
                                    Role modelRole = context.Role.Where(r => r.ID == role.RoleInfo.ID).FirstOrDefault();

                                    if (modelRole.Name == ConfigurationManager.AppSettings["adminRole"])
                                    {
                                        dbUser.IsSuperAdmin = true;
                                    }

                                    dbUser.Roles.Add(modelRole);
                                }

                                dbUser.IsFirstTimeLogin = dbUser.IsSuperAdmin ? false : true;
                                dbUser.Username = user.UserInfo.Email;
                                dbUser.Email = user.UserInfo.Email;

                                var result = await UserManager.CreateAsync(appUser, user.Password);
                                if (result.Succeeded)
                                {
                                    dbUser.Mobile = user.UserInfo.Mobile;
                                    dbUser.CreatedBy = UserName;
                                    dbUser.CreatedOn = DateTime.Now;
                                    context.User.Add(dbUser);
                                    await context.SaveChangesAsync();

                                    if (!dbUser.IsSuperAdmin)
                                    {
                                        foreach (var item in user.Companies)
                                        {
                                            var cID = Convert.ToInt32(item);
                                            context.UserCompanyMappings.Add(new UserCompanyMapping
                                            {
                                                CompanyId = cID,
                                                UserId = dbUser.ID
                                            });

                                            await context.SaveChangesAsync();
                                        }
                                    }

                                    TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

                                    return RedirectToAction("Index");
                                }

                                AddErrors(result);
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("", "User creation failed.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
            }

            user.Roles = ViewModels.Roles.RoleVM.ConvertAll(roleMasterService.Value.GetAll().OrderBy(r => r.Name).ToList());
            Load();
            return View(user);
        }

        public async Task<ActionResult> Edit(int id)
        {
            User user = userMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();

            //Clients cannot be modified through the User Edit screen

            ApplicationUser appUser = await UserManager.FindByEmailAsync(user.Email);

            ViewModels.UserVM model = new ViewModels.UserVM();
            model.UserInfo = user;
            model.Mobile = appUser.PhoneNumber;
            model.Password = "dummypassword";
            model.ConfirmPassword = "dummypassword";
            model.Roles = ViewModels.Roles.RoleVM.ConvertAll(roleMasterService.Value.GetAll().OrderBy(r => r.Name).ToList());
            foreach (Role role in user.Roles)
            {
                model.Roles.Find(p => p.RoleInfo.Name == role.Name).IsSelected = true;
            }

            Load();

            var userComapnies = userCompanyMappingService.Value.FindBy(x => x.UserId == id).Select(x => x.CompanyId).ToList();

            foreach (var item in userComapnies)
            {
                model.Companies.Add(item.ToString());
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ViewModels.UserVM user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User dbUser = userMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();

                    string email = dbUser.Email;
                    dbUser.Name = user.UserInfo.Name;
                    dbUser.Email = user.UserInfo.Email;
                    dbUser.Username = user.UserInfo.Email;
                    dbUser.Mobile = user.UserInfo.Mobile;
                    dbUser.Roles.Clear();
                    List<ViewModels.Roles.RoleVM> userRoles = user.Roles.Where(p => p.IsSelected).ToList();
                    foreach (ViewModels.Roles.RoleVM role in userRoles)
                    {
                        Role modelRole = roleMasterService.Value.FindBy(x => x.ID == role.RoleInfo.ID).SingleOrDefault();
                        if (modelRole.Name == ConfigurationManager.AppSettings["adminRole"])
                        {
                            dbUser.IsSuperAdmin = true;
                        }
                        dbUser.Roles.Add(modelRole);
                    }

                    dbUser.IsFirstTimeLogin = dbUser.IsSuperAdmin ? false : true;
                    dbUser.ModifiedBy = UserName;
                    dbUser.ModifiedOn = DateTime.Now;
                    userMasterService.Value.Edit(dbUser);

                    var dbUserCompany = userCompanyMappingService.Value.FindBy(x => x.UserId == dbUser.ID).ToList();

                    foreach (var item in dbUserCompany)
                    {
                        userCompanyMappingService.Value.Delete(item);
                        userCompanyMappingService.Value.Save();
                    }

                    foreach (var item in user.Companies)
                    {
                        var companyId = Convert.ToInt32(item);
                        if (userCompanyMappingService.Value.FindBy(x => x.CompanyId == companyId && x.UserId == dbUser.ID).FirstOrDefault() == null)
                        {
                            UserCompanyMapping userCompanyMapping = new UserCompanyMapping
                            {
                                UserId = dbUser.ID,
                                CompanyId = companyId
                            };

                            userCompanyMappingService.Value.Add(userCompanyMapping);
                            userCompanyMappingService.Value.Save();
                        }
                    }

                    ApplicationUser appUser = await UserManager.FindByEmailAsync(email);

                    appUser.Email = user.UserInfo.Email;
                    appUser.UserName = user.UserInfo.Email;
                    appUser.PhoneNumber = user.Mobile;

                    await UserManager.UpdateAsync(appUser);

                    TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

                    return RedirectToAction("Index");
                }
                else
                {
                    Load();

                    user.Roles = ViewModels.Roles.RoleVM.ConvertAll(roleMasterService.Value.GetAll().OrderBy(r => r.Name).ToList());

                    return View(user);
                }
            }
            catch (Exception ex)
            {
                Load();

                ModelState.AddModelError("", "Internal exception");

                user.Roles = ViewModels.Roles.RoleVM.ConvertAll(roleMasterService.Value.GetAll().OrderBy(r => r.Name).ToList());

                return View(user);
            }
        }

        public ActionResult Delete(int id)
        {
            User user = userMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, User user)
        {
            try
            {
                var model = userMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();
                userMasterService.Value.Delete(model);

                await UserManager.DeleteAsync(await UserManager.FindByEmailAsync(user.Username));

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                user = userMasterService.Value.FindBy(x => x.ID == id).SingleOrDefault();

                TempData["error-message"] = ConstantValue.MASTER_DELETE_ERROR_MESSAGE;

                return RedirectToAction("Index");
            }
        }

        private void Load()
        {
            ViewBag.DBCompanies = companyService.Value.FindBy(x => x.Status == true)
                                                    .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                                                    .ToList();
        }
    }
}