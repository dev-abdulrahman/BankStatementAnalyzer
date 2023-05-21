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
    public class MenuController : BaseController
    {
        private readonly IMenuService menuService;
        private readonly ILoggerFacade<BaseController> logger;

        public MenuController(IMenuService menuService,
            ILoggerFacade<BaseController> logger,
            IUserMasterService userMasterService) : base(userMasterService, logger)
        {
            this.menuService = menuService;
            this.logger = logger;
        }

        // GET: Menu
        public ActionResult Index()
        {
            return View(menuService.GetAll().OrderByDescending(x => x.ID).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            load();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (string.IsNullOrWhiteSpace(menu.Name) && string.IsNullOrWhiteSpace(menu.DisplayName))
            {
                load();

                TempData["warning-message"] = "Name and Display name can not be empty.";

                return View(menu);
            }

            if (menuService.FindBy(x => x.Name == menu.Name).FirstOrDefault() != null)
            {
                load();

                TempData["warning-message"] = "Menu with same name already exist.";

                return View(menu);
            }

            var sortOrder = getSortOrder(menu);

            menu.Order = sortOrder;
            menu.CreatedBy = UserName;

            menuService.Add(menu);
            menuService.Save();

            TempData["success-message"] = "Menu added successfully.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            load();

            Menu model = getMenuByID(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, Menu menu)
        {
            if (string.IsNullOrWhiteSpace(menu.Name) && string.IsNullOrWhiteSpace(menu.DisplayName))
            {
                load();

                TempData["warning-message"] = "Name and Display name can not be empty.";

                return View(menu);
            }

            var oldRecord = getMenuByID(id);

            if (oldRecord.Name == menu.Name && oldRecord.DisplayName == menu.DisplayName)
            {
                oldRecord.Icon = menu.Icon;
                oldRecord.ActionName = menu.ActionName;
                oldRecord.Controller = menu.Controller;
                oldRecord.ParentId = menu.ParentId;

                oldRecord.UpdatedBy = UserName;
                oldRecord.UpdatedDate = DateTime.Now;

                menuService.Edit(oldRecord);
                menuService.Save();
            }
            else
            {
                var allMenu = menuService.FindBy(x => x.Name != oldRecord.Name && x.DisplayName != oldRecord.DisplayName).ToList();
                var isExist = allMenu.Any(x => x.Name == menu.Name && x.DisplayName == menu.DisplayName);

                if (isExist)
                {
                    load();

                    TempData["warning-message"] = "Menu with same name already exist.";

                    return View(menu);
                }

                else
                {
                    oldRecord.Icon = menu.Icon;
                    oldRecord.ActionName = menu.ActionName;
                    oldRecord.Controller = menu.Controller;
                    oldRecord.ParentId = menu.ParentId;

                    oldRecord.UpdatedBy = UserName;
                    oldRecord.UpdatedDate = DateTime.Now;

                    menuService.Edit(oldRecord);
                    menuService.Save();
                }
            }

            TempData["success-message"] = "Menu edited successfully.";

            return RedirectToAction("Index");
        }

        private void load()
        {
            ViewBag.Parent = menuService
                            .GetAll()
                            .Where(x => x.ActionName == null && x.Controller == null)
                            .OrderBy(x => x.Order)
                            .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                            .ToList();
        }

        private Menu getMenuByID(int id)
        {
            return menuService.FindBy(x => x.ID == id).FirstOrDefault();
        }

        private int getSortOrder(Menu menu)
        {
            var result = 0;
            if (menu.ParentId == null)
            {
                var response = menuService.FindBy(x => x.ParentId == menu.ParentId).OrderByDescending(x => x.ID).FirstOrDefault();

                result = response != null ? response.Order + 1 : 0;

                return result;
            }
            else
            {
                var response = menuService.FindBy(x => x.ParentId == menu.ParentId).OrderByDescending(x => x.ID).FirstOrDefault();

                result = response != null ? response.Order + 1 : 0;

                return result;
            }
        }
    }
}