using BankStatementAnalyzer.BusinessLogic.Interface;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [AllowAnonymous]
    public class SupportController : Controller
    {

        private readonly IAppMessageService appMessageService;

        public SupportController(IAppMessageService appMessageService)
        {
            this.appMessageService = appMessageService;
        }

        public ActionResult Index()
        {
            var email = appMessageService.FindBy(x => x.Key == "email_contact").FirstOrDefault();
            var phone = appMessageService.FindBy(x => x.Key == "phone_contact").FirstOrDefault();

            return View(new Tuple<string, string>(email.Message, phone.Message));
        }
    }
}