using BankStatementAnalyzer.BusinessLogic.Interface;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class AmountDueController : ApiController
    {
        private readonly ICustomerService customerService;
        private readonly ICompanyService companyService;
        private readonly ICustomerCreditMappingService customerCreditMappingService;
        private readonly ISystemSettingService systemSettingService;

        public AmountDueController(ICustomerService customerService,
            ICompanyService companyService,
            ICustomerCreditMappingService customerCreditMappingService,
            ISystemSettingService systemSettingService)
        {
            this.customerService = customerService;
            this.customerCreditMappingService = customerCreditMappingService;
            this.companyService = companyService;
            this.systemSettingService = systemSettingService;
        }

        [Route("api/AmountDue/Get")]
        public HttpResponseMessage Get(string customerId, string companyKey)
        {
            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
            }

            var user = customerService.FindBy(x => x.UID == customerId).FirstOrDefault();

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            if (user.IsCreditApplicable)
            {
                var sysVal = systemSettingService.FindBy(x => x.SettingType == BankStatementAnalyzer.Models.SettingType.DefaultCreditValue).FirstOrDefault();

                var credit = customerCreditMappingService.FindBy(x => x.CustomerId == user.Id).FirstOrDefault();

                var CreditAmount = credit?.CreditAmount == null ? Convert.ToDecimal(sysVal.Value) : credit?.CreditAmount.Value;

                return Request.CreateResponse(HttpStatusCode.OK, new { CreditAmount, credit.AdvancePayment, credit.AmountDue });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "No credit available." });
        }
    }
}