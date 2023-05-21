using BankStatementAnalyzer.BusinessLogic.Interface;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class NewCanBasePriceController : ApiController
    {
        private readonly ISystemSettingService systemSettingService;

        public NewCanBasePriceController(ISystemSettingService systemSettingService)
        {
            this.systemSettingService = systemSettingService;
        }

        [Route("api/NewCanBasePrice/Get")]
        public HttpResponseMessage Get()
        {
            var val = systemSettingService.FindBy(x => x.SettingType == BankStatementAnalyzer.Models.SettingType.NewCanBasePrice).FirstOrDefault();

            if (val != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { val.Value });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "No amount configured" });
        }
    }
}