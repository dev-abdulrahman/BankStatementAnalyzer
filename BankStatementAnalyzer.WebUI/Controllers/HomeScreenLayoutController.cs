using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using BankStatementAnalyzer.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class HomeScreenLayoutController : BaseController
    {
        private readonly IUserMasterService userMasterService;
        private readonly ILoggerFacade<BaseController> logger;
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<IIconsService> iconsService;
        private readonly Lazy<ISystemSettingService> systemSettingService;
        private readonly Lazy<IColorsService> colorsService;
        private readonly Lazy<IHomeScreenLayoutService> homeScreenLayoutService;
        private readonly Lazy<ISubCategoryService> subcategoryService;

        public HomeScreenLayoutController(IUserMasterService userMasterService, 
            ILoggerFacade<BaseController> logger,
            Lazy<ICategoryService> categoryService,
            Lazy<IIconsService> iconsService,
            Lazy<ISystemSettingService> systemSettingService,
            Lazy<IColorsService> colorsService,
            Lazy<IHomeScreenLayoutService> homeScreenLayoutService,
            Lazy<ISubCategoryService> subcategoryService) : base(userMasterService, logger)
        {
            this.userMasterService = userMasterService;
            this.logger = logger;
            this.categoryService = categoryService;
            this.iconsService = iconsService;
            this.systemSettingService = systemSettingService;
            this.colorsService = colorsService;
            this.homeScreenLayoutService = homeScreenLayoutService;
            this.subcategoryService = subcategoryService;
        }

        // GET: CategoryIcons
        [HttpGet]
        public ActionResult Index()
        {
            var categories = categoryService.Value.GetAll().ToList();
            var columnValue = systemSettingService.Value.GetAll().Where(x => x.SettingTypeValue == "GridColumns").FirstOrDefault();

            ViewBag.Rows = Math.Ceiling(categories.Count() / Convert.ToDouble(columnValue?.Value));

            List<CategoryIconViewModel> model = new List<CategoryIconViewModel>();
            int row = 1, col = 1;

            for (int i = 0; i < categories.Count(); i++)
            {
                CategoryIconViewModel iconModel = new CategoryIconViewModel();
                iconModel.ID = categories[i].ID;
                iconModel.Name = categories[i].Name;
                iconModel.Row = row;
                iconModel.Column = col;
                model.Add(iconModel);
                if (col == Convert.ToInt32(columnValue?.Value))
                {
                    row += 1;
                    col = 1;
                }
                else
                {
                    col++;
                }
            }

            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int ID, int row, int col)
        {
            var categories = categoryService.Value.GetAll();
            var icons = iconsService.Value.GetAll().ToList();
            var colors = colorsService.Value.GetAll().ToList();
            HomeScreenViewModel model = new HomeScreenViewModel()
            {
                Row = row,
                Column = col,
                Category = categories?.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name.ToString() }).ToList(),
                SelectedCategory = ID,
                Icons = icons?.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.ClassName.ToString()}).ToList(),
                Color = colors?.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name.ToString() }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(HomeScreenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return RedirectToAction("Index", "HomeScreenLayout");
            }

            var updateRecord = homeScreenLayoutService.Value.FindBy(x => x.Row == model.Row && x.Column == model.Column).FirstOrDefault();
            if (updateRecord != null)
            {
                updateRecord.IconId = model.SelectedIcon;
                updateRecord.Row = model.Row;
                updateRecord.Column = model.Column;
                updateRecord.Status = true;
                updateRecord.Category = categoryService.Value.FindBy(x => x.ID == model.SelectedCategory).FirstOrDefault();
                updateRecord.Color = colorsService.Value.FindBy(x => x.ID == model.SelectedColor).FirstOrDefault();
                homeScreenLayoutService.Value.Edit(updateRecord);
            }
            else
            {
                updateRecord = new HomeScreenLayout()
                {
                    IconId = model.SelectedIcon,
                    Row = model.Row,
                    Column = model.Column,
                    Status = true,
                    Category = categoryService.Value.FindBy(x => x.ID == model.SelectedCategory).FirstOrDefault(),
                    Color = colorsService.Value.FindBy(x => x.ID == model.SelectedColor).FirstOrDefault()
                };
                homeScreenLayoutService.Value.Add(updateRecord);
            }
            homeScreenLayoutService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index", "HomeScreenLayout");
        }

    }
}