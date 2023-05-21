using BankStatementAnalyzer.BusinessLogic.Interface;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class DeliveryChargeController : ApiController
    {
        private readonly ICompanyService companyService;
        private readonly ISystemSettingService systemSettingService;

        public DeliveryChargeController(ICompanyService companyService, ISystemSettingService systemSettingService)
        {
            this.companyService = companyService;
            this.systemSettingService = systemSettingService;
        }

        [HttpGet]
        [Route("api/DeliveryCharge/Get")]
        public HttpResponseMessage Get(string companyKey)
        {
            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();
            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
            }

            var sysVal = systemSettingService.FindBy(x => x.SettingType == BankStatementAnalyzer.Models.SettingType.DeliveryCharges).FirstOrDefault();

            var charge = sysVal == null ? 0 : Convert.ToDouble(sysVal.Value);

            return Request.CreateResponse(HttpStatusCode.OK, new { charge });
        }
    }
}