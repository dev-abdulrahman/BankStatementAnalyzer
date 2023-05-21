using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.SuperAdmin.Filters;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace BankStatementAnalyzer.SuperAdmin.Controllers
{
    [RouteAuthorize]
    public class BaseController : Controller
    {
        private readonly IUserMasterService userMasterService;
        public BaseController(IUserMasterService userMasterService)
        {
            this.userMasterService = userMasterService;
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

        protected string GetRoleName()
        {
            return GetRole().Name;
        }
    }
}