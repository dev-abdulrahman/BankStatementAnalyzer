using BankStatementAnalyzer.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BankStatementAnalyzer.SuperAdmin.Filters
{
    public class RouteAuthorizeAttribute : AuthorizeAttribute
    {
        public IPermissionMasterService permissionMasterService { get; set; }

        public string RouteDataController { get; set; } = "controller";

        public string RouteDataAction { get; set; } = "action";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            RequestContext requestContext = httpContext.Request.RequestContext;

            IPrincipal user = httpContext.User;

            if (!user.Identity.IsAuthenticated)
                return false;

            if (!requestContext.RouteData.Values.ContainsKey(RouteDataController))
                throw new ApplicationException("RouteDataKey " + RouteDataController + " does not exist in the current RouteData");

            if (!requestContext.RouteData.Values.ContainsKey(RouteDataAction))
                throw new ApplicationException("RouteDataKey " + RouteDataAction + " does not exist in the current RouteData");

            string activity = "ACTION." + requestContext.RouteData.Values[RouteDataController].ToString() + "." + requestContext.RouteData.Values[RouteDataAction].ToString();
            List<string> permissions = permissionMasterService.GetAllForUser(user.Identity.Name);
            var result = permissions.Contains(activity);
            return result;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Account",
                                action = "Unauthorized"
                            })
                        );
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Account",
                                action = "Login"
                            })
                        );
            }
        }
    }
}