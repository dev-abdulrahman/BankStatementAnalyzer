using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.BusinessLogic.ViewModels;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.WebUI.App_Start;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static BankStatementAnalyzer.WebUI.ViewModels.AccountViewModel;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private readonly int ROLE_SUPER_ADMINISTRATOR_ID = 1;
        private readonly IUserMasterService userMasterService;
        private readonly IRoleMasterService roleService;
        private readonly IMemoryCacheService memoryCacheService;
        private readonly ILoggerFacade<BaseController> logger;

        public AccountController(IUserMasterService userMasterService,
            IRoleMasterService roleService,
            IMemoryCacheService memoryCacheService,
            ILoggerFacade<BaseController> logger) : base(userMasterService, logger)
        {
            this.userMasterService = userMasterService;
            this.roleService = roleService;
            this.memoryCacheService = memoryCacheService;
            this.logger = logger;
        }

        public AccountController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger) : base(userMasterService, logger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private const string RememberMeCookieName = ".History";

        private string CheckForCookieUserName()
        {
            string returnValue = string.Empty;
            HttpCookie rememberMeUserNameCookie = Request.Cookies.Get(RememberMeCookieName);
            if (null != rememberMeUserNameCookie)
            {
                /* Note, the browser only sends the name/value to the webserver, and not the expiration date */
                returnValue = Base64Decode(rememberMeUserNameCookie.Value);
            }

            return returnValue;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.FirstUser = userMasterService.GetAll().Count() == 0;

            LoginViewModel model = new LoginViewModel();

            string username = CheckForCookieUserName();
            if (!string.IsNullOrWhiteSpace(username))
            {
                model.Email = username;
                model.RememberMe = true;
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.FirstUser = userMasterService.GetAll().Count() == 0;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.RememberMe)
            {
                HttpCookie rememberMeCookie = new HttpCookie(RememberMeCookieName, Base64Encode(model.Email))
                {
                    Expires = DateTime.MaxValue
                };
                Response.SetCookie(rememberMeCookie);
            }
            else
            {
                HttpCookie rememberMeCookie = new HttpCookie(RememberMeCookieName, null)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.SetCookie(rememberMeCookie);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email.ToLowerInvariant(), model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.Email, returnUrl);

                case SignInStatus.LockedOut:

                    return View("Lockout");

                case SignInStatus.RequiresVerification:

                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });
                case SignInStatus.Failure:

                default:
                    ModelState.AddModelError("", "Invalid login attempt.");

                    return View(model);
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogOff");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(LocalPasswordModel model)
        {
            ViewBag.ReturnUrl = Url.Action("ChangePassword");
            var loggedInUser = GetLoggedInUser();

            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                try
                {
                    if (loggedInUser.Username.ToLower().Trim() == model.NewPassword.ToLower().Trim())
                    {
                        ModelState.AddModelError("", "The user name and password should not be same.");
                        return View(model);
                    }

                    var user = await UserManager.FindByNameAsync(loggedInUser.Username);
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var result = await UserManager.ResetPasswordAsync(user.Id, token, model.NewPassword);

                    if (result.Succeeded)
                    {
                        await UserManager.UpdateAsync(user);

                        loggedInUser.IsFirstTimeLogin = false;
                        loggedInUser.ModifiedBy = UserName;
                        loggedInUser.ModifiedOn = DateTime.Now;

                        userMasterService.Edit(loggedInUser);
                        userMasterService.Save();

                        TempData["success-message"] = "Your passowrd has been changed successfully";

                        return RedirectToAction("LogOff");
                    }
                    else
                    {
                        AddErrors(result);

                        TempData["error-message"] = "The current password is incorrect or the new password is invalid.";

                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);

                    TempData["error-message"] = "The current password is incorrect or the new password is invalid.";
                    return RedirectToAction("ChangePassword");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult RedirectToLocal(string username, string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            Role role = userMasterService.GetUserByUsername(username).Roles.FirstOrDefault();

            return RedirectToDefaultPage(role.ID);
        }

        private ActionResult RedirectToDefaultPage(int roleID)
        {
            Role role = roleService.FindBy(x => x.ID == roleID).SingleOrDefault();

            return RedirectToAction(role.DefaultAction, role.DefaultController);
        }

        [AllowAnonymous]
        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            Session.Clear();

            clearMemoryCache(Request.GetOwinContext().Request.User.Identity.Name);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Login", "Account");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void clearMemoryCache(string username)
        {
            List<string> memoryCacheKeys = MemoryCacheKeys.GetAllForUser(username);

            foreach (string memoryCacheKey in memoryCacheKeys)
            {
                memoryCacheService.Clear(memoryCacheKey);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();

            return View(FlowView(), model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            switch (Flow())
            {
                case RegistrationFlow.Admin:
                    return await RegisterAdmin(model);
            }
            return null;
        }

        public enum RegistrationFlow { Admin, Default }

        private string FlowView()
        {
            switch (Flow())
            {
                case RegistrationFlow.Admin:
                    return "RegisterAdmin";

                default:
                    return "RegisterAdmin";
            }
        }

        private RegistrationFlow Flow()
        {
            if (userMasterService.GetAll().Count() == 0)
            {
                return RegistrationFlow.Admin;
            }
            return RegistrationFlow.Default;
        }


        private async Task<ActionResult> RegisterAdmin(RegisterViewModel model)
        {
            ModelState["Code"]?.Errors.Clear();
            ModelState["Gender"]?.Errors.Clear();
            ModelState["DateOfBirth"]?.Errors.Clear();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email.ToLowerInvariant(), Email = model.Email, PhoneNumber = model.Mobile };
                User appUser = new User();

                using (var context = new BankStatementAnalyzerContext())
                {
                    try
                    {
                        appUser.Name = model.Name;
                        Role adminRole = context.Role.Where(r => r.ID == ROLE_SUPER_ADMINISTRATOR_ID).FirstOrDefault();

                        if (adminRole != null)
                        {
                            //First user will be an Administrator
                            appUser.Roles.Add(adminRole);

                            appUser.Username = model.Email;
                            appUser.Email = model.Email;
                            appUser.IsSuperAdmin = true;

                            var result = await UserManager.CreateAsync(user, model.Password);
                            if (result.Succeeded)
                            {
                                appUser.CreatedOn = DateTime.Now;
                                context.User.Add(appUser);
                                await context.SaveChangesAsync();

                            }

                            AddErrors(result);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Registration failed. Roles missing.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "User registration failed.");
                    }
                }

                if (appUser.ID > 0)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToDefaultPage(appUser.Roles.FirstOrDefault().ID);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(FlowView(), model);
        }
    }
}