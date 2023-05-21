using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class TimeSlotController : ApiController
    {
        private readonly ITimeSlotService timeSlotService;
        private readonly ICompanyService companyService;

        public TimeSlotController(ITimeSlotService timeSlotService,
            ICompanyService companyService)
        {
            this.timeSlotService = timeSlotService;
            this.companyService = companyService;
        }

        [Route("api/TimsSlot/Get")]
        [HttpGet]
        public HttpResponseMessage Get(string companyKey)
        {
            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
            }

            var timeSlots = timeSlotService.GetAll().Where(x => x.Status == true);

            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();

            foreach (var timeSlot in timeSlots)
            {
                var response = HtmlHelpers.GetTimeFromAndTo(timeSlot.From, timeSlot.To);

                keyValuePairs.Add(timeSlot.ID, response);
            }

            return Request.CreateResponse(HttpStatusCode.OK, keyValuePairs);
        }
    }
}