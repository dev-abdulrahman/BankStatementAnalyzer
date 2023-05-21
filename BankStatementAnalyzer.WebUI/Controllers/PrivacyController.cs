using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [AllowAnonymous]
    public class PrivacyController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}