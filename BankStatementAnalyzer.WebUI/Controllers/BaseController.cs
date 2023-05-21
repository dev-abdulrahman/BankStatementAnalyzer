using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using BankStatementAnalyzer.WebUI.Helpers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class BaseController : Controller
    {
        private readonly IUserMasterService userMasterService;
        private readonly ILoggerFacade<BaseController> logger;

        public BaseController(IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger)
        {
            this.userMasterService = userMasterService;
            this.logger = logger;
        }

        public ClaimsIdentity ClaimIdentity
        {
            get
            {
                return ((ClaimsIdentity)(HttpContext.User.Identity));
            }
        }

        protected string UserName
        {
            get
            {
                var identity = ClaimIdentity;
                var claim = identity.Claims.Where(c => c.Type == identity.NameClaimType).FirstOrDefault();
                return claim.Value.ToString();
            }
        }

        protected User GetLoggedInUser()
        {
            var dbUser = userMasterService.FindBy(x => x.Username == UserName).FirstOrDefault();
            return dbUser;
        }

        protected Role GetRole()
        {
            return GetLoggedInUser().Roles.FirstOrDefault();
        }

        protected int GetCompanyId()
        {
            var company = Session["Company"] == null ? null : Session["Company"] as Company;
            return company == null ? 0 : company.ID;
        }

        protected string GetRoleName()
        {
            return GetRole().Name;
        }

        #region Exception Handling
        public static log4net.ILog Logger
        {
            get
            {
                return Startup.Logger;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            Logger.Error($"Controller - {RequestHelper.Controller()} : Action - {RequestHelper.Action()}", filterContext.Exception);

            TempData["error-message"] = ConstantValue.MASTER_ERROR_MESSAGE;
            filterContext.Result = RedirectToAction("Error", "Home");
        }
        #endregion
    }
}