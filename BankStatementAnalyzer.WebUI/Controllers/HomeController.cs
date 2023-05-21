using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.BusinessLogic.ViewModels;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class HomeController : BaseController
    {
        private readonly Lazy<IUserMasterService> userMasterService;

        private readonly Lazy<IMenuService> menuService;

        private readonly Lazy<IDashboardService> dashboardService;

        private readonly Lazy<ILoggerFacade<BaseController>> logger;

        public HomeController(
            Lazy<IUserMasterService> userMasterService,
            Lazy<IMenuService> menuService,
            Lazy<IDashboardService> dashboardService,
            Lazy<ILoggerFacade<BaseController>> logger)
            : base(userMasterService.Value, logger.Value)
        {
            this.userMasterService = userMasterService;
            this.menuService = menuService;
            this.dashboardService = dashboardService;
        }

        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index()
        {
            List<Dashboard> dashboards = new List<Dashboard>();
            var customie = dashboardService.Value.GetCustomizeCount();
            dashboards.Add(customie);

            var whatsApp = dashboardService.Value.GetWhatsAppCount();
            dashboards.Add(whatsApp);

            var oneTouch = dashboardService.Value.GetOneTouchCount();
            dashboards.Add(oneTouch);

            var package = dashboardService.Value.GetPackageCount();
            dashboards.Add(package);

            return View(dashboards);
        }

        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var role = GetRole();

            var menu = menuService.Value.GetAllForUser(UserName, role?.Name == "Super Administrator" ? true : false);

            return PartialView("_Menu", menu);
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }
    }
}