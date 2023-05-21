using BankStatementAnalyzer.SuperAdmin.Filters;
using System.Web.Mvc;

namespace BankStatementAnalyzer.SuperAdmin.Controllers
{
    [RouteAuthorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}