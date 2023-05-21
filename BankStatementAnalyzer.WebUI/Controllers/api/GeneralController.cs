using BankStatementAnalyzer.BusinessLogic.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class GeneralController : ApiController
    {
        private readonly ISupportAndFAQService supportAndFAQService;
        private readonly ISystemSettingService systemSettingService;

        public GeneralController(ISupportAndFAQService supportAndFAQService,
            ISystemSettingService systemSettingService)
        {
            this.supportAndFAQService = supportAndFAQService;
            this.systemSettingService = systemSettingService;
        }

        [HttpGet]
        [Route("api/General/FAQs")]
        public HttpResponseMessage FAQs()
        {
            var faq = ConfigurationManager.AppSettings["faqMaxVal"];
            var faqVal = string.IsNullOrWhiteSpace(faq) ? 10 : Convert.ToInt32(faq);
            var dbFAQs = supportAndFAQService.FindBy(x => x.FAQType == BankStatementAnalyzer.Models.FAQType.Customer).Take(faqVal).ToList();

            FAQS fAQS = new FAQS();
            foreach (var item in dbFAQs)
            {
                FAQ fAQ = new FAQ
                {
                    Question = item.Title,
                    Answer = item.Description
                };

                fAQS.FAQ.Add(fAQ);
            }

            return Request.CreateResponse(HttpStatusCode.OK, fAQS);
        }

        [HttpGet]
        [Route("api/General/HelpText")]
        public HttpResponseMessage HelpText()
        {
            var helpTextReferal = systemSettingService.FindBy(x => x.SettingType == BankStatementAnalyzer.Models.SettingType.ReferalPointHelpText).FirstOrDefault();

            Dictionary<string, List<object>> valuePairs = new Dictionary<string, List<object>>();

            if (helpTextReferal != null)
            {
                string[] actualReferalText = helpTextReferal.Value.Split('|');

                List<object> list = new List<object>();

                foreach (var item in actualReferalText)
                {
                    list.Add(new { Title = item });
                }

                valuePairs.Add("PointsOnReferral", list);
            }

            return Request.CreateResponse(HttpStatusCode.OK, valuePairs);
        }

        public class FAQS
        {
            public FAQS()
            {
                FAQ = new List<FAQ>();
            }

            public List<FAQ> FAQ { get; set; }
        }

        public class FAQ
        {
            public string Question { get; set; }

            public string Answer { get; set; }
        }
    }
}