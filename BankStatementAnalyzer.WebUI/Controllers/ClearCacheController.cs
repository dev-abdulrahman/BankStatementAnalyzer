using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.BusinessLogic.ViewModels;
using BankStatementAnalyzer.WebUI.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class ClearCacheController : Controller
    {
        private readonly IMemoryCacheService memoryCacheService;
        private readonly IUserMasterService userMasterService;

        public ClearCacheController(IMemoryCacheService memoryCacheService,
            IUserMasterService userMasterService)
        {
            this.userMasterService = userMasterService;
            this.memoryCacheService = memoryCacheService;
        }

        [HttpGet]
        public ActionResult Clear()
        {
            return View();
        }

        public ActionResult Clear(int id = 0)
        {
            var users = userMasterService.GetAll().ToList();
            var username = users != null && users.Count() > 0 ? users.Select(x => x.Username).ToList() : null;

            foreach (var item in username)
            {
                List<string> memoryCacheKeys = MemoryCacheKeys.GetAllForUser(item);

                foreach (string memoryCacheKey in memoryCacheKeys)
                {
                    memoryCacheService.Clear(memoryCacheKey);
                }
            }

            TempData["success-message"] = "Cache cleared successfully.";

            return RedirectToAction("Index", "Home");
        }
    }
}