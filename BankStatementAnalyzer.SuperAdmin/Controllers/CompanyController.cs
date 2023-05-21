using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.SuperAdmin.Common.constants;
using BankStatementAnalyzer.SuperAdmin.Common.logger;
using BankStatementAnalyzer.SuperAdmin.Filters;
using BankStatementAnalyzer.SuperAdmin.ViewModels.UsersVM;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.SuperAdmin.Controllers
{
    [RouteAuthorize]
    public class CompanyController : BaseController
    {
        private readonly Lazy<ILoggerFacade<CompanyController>> logger;
        private readonly Lazy<ICompanyService> companyService;
        private readonly Lazy<IUserMasterService> userMasterService;
        private readonly Lazy<IUserCompanyMappingService> userCompanyMappingService;

        public CompanyController(Lazy<IUserMasterService> userMasterService,
            Lazy<ILoggerFacade<CompanyController>> logger,
            Lazy<ICompanyService> companyService,
            Lazy<IUserCompanyMappingService> userCompanyMappingService) : base(userMasterService.Value)
        {
            this.logger = logger;
            this.companyService = companyService;
            this.userMasterService = userMasterService;
            this.userCompanyMappingService = userCompanyMappingService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(companyService.Value.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Company company = new Company { Status = true };

            return View(company);
        }

        [HttpPost]
        public ActionResult Create(Company company)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(company);
            }

            if (companyService.Value.FindBy(x => x.Name == company.Name && x.Code == company.Code).FirstOrDefault() != null)
            {
                TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                return View(company);
            }

            company.CreatedBy = UserName;
            company.Key = company.Name.Replace(" ", "");
            companyService.Value.Add(company);
            companyService.Value.Save();

            UserCompanyMapping userCompanyMapping = new UserCompanyMapping
            {
                CompanyId = company.ID,
                UserId = GetLoggedInUser().ID
            };

            #region Map company and admin user
            userCompanyMappingService.Value.Add(userCompanyMapping);
            userCompanyMappingService.Value.Save();
            #endregion

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            if (company.CreateAnother)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = companyService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Company company, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(company);
            }

            var dbCompany = companyService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            if (dbCompany.Name != company.Name && dbCompany.Code != company.Code)
            {
                var isExist = companyService.Value.FindBy(x => x.Name == company.Name && x.Code == company.Code).FirstOrDefault() != null;

                if (isExist)
                {
                    TempData["warning-message"] = ConstantValue.MASTER_EDIT_DUPLICATE_RECORD_MESSAGE;

                    return View(company);
                }
            }

            dbCompany.ModifiedBy = UserName;
            dbCompany.ModifiedOn = DateTime.Now;
            dbCompany.Code = company.Code;
            dbCompany.Name = company.Name;
            dbCompany.Key = company.Name.Replace(" ", "");
            dbCompany.Status = company.Status;

            companyService.Value.Edit(dbCompany);
            companyService.Value.Save();

            TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = companyService.Value.FindBy(x => x.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, Company company)
        {
            try
            {
                var usercomapny = userCompanyMappingService.Value.FindBy(x => x.CompanyId == id).ToList();

                if (usercomapny.Count > 0)
                {
                    TempData["error-message"] = ConstantValue.MASTER_DELETE_ERROR_MESSAGE;

                    return RedirectToAction("Index");
                }

                var model = companyService.Value.FindBy(x => x.ID == id).FirstOrDefault();

                model.Status = false;
                model.ModifiedOn = DateTime.Now;
                model.ModifiedBy = UserName;

                companyService.Value.Edit(model);
                companyService.Value.Save();

                TempData["success-message"] = ConstantValue.MASTER_DELETE_SUCCSESS_MESSAGE;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Value.Error(ex.Message, ex);

                TempData["error-message"] = ConstantValue.MASTER_DELETE_ERROR_MESSAGE;

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult MapUser(int id)
        {
            Load(id);

            UserCompany userCompany = new UserCompany();
            var userCompanylist = userCompanyMappingService.Value.FindBy(x => x.CompanyId == id).ToList();

            foreach (var item in userCompanylist)
            {
                var user = userMasterService.Value.FindBy(x => x.ID == item.UserId).FirstOrDefault();

                MappedUser mappedUser = new MappedUser
                {
                    UserName = user?.Username
                };

                userCompany.MappedUsers.Add(mappedUser);
            }

            return View(userCompany);
        }

        [HttpPost]
        public ActionResult MapUser(UserCompany userCompany, int id)
        {
            foreach (var item in userCompany.Users)
            {
                var userid = Convert.ToInt32(item);
                if (userCompanyMappingService.Value.FindBy(x => x.CompanyId == id && x.UserId == userid).FirstOrDefault() == null)
                {
                    UserCompanyMapping userCompanyMapping = new UserCompanyMapping
                    {
                        CompanyId = id,
                        UserId = userid
                    };

                    userCompanyMappingService.Value.Add(userCompanyMapping);
                    userCompanyMappingService.Value.Save();
                }
            }

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        private void Load(int companyid = 0)
        {
            var userIds = userCompanyMappingService.Value.GetAll().Where(x => x.CompanyId == companyid).ToList();

            var users = userMasterService.Value.FindBy(x => x.IsSuperAdmin == false).ToList();

            ViewBag.Users = users.Where(p => userIds.All(p2 => p2.UserId != p.ID))
                               .Select(x => new SelectListItem { Text = x.Username, Value = x.ID.ToString() }).ToList();
        }
    }
}