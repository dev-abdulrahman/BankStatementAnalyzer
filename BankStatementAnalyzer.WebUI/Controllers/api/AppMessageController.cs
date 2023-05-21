using BankStatementAnalyzer.BusinessLogic.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class AppMessageController : ApiController
    {
        private readonly IAppMessageService appMessageService;

        public AppMessageController(IAppMessageService appMessageService)
        {
            this.appMessageService = appMessageService;
        }

        [HttpGet]
        [Route("api/AppMessage/Get")]
        public HttpResponseMessage Get(string key)
        {
            var message = appMessageService.FindBy(x => x.Key == key).FirstOrDefault();

            var result = message == null ? "" : message.Message;

            return Request.CreateResponse(HttpStatusCode.OK, new { Message = result });
        }

        [HttpGet]
        [Route("api/AppMessage/GetAll")]
        public HttpResponseMessage GetAll()
        {
            List<Response> responses = new List<Response>();

            var messages = appMessageService.GetAll();

            foreach (var message in messages)
            {
                Response response = new Response { Key = message.Key, Message = message.Message };

                responses.Add(response);
            }

            return Request.CreateResponse(HttpStatusCode.OK, responses);
        }

        public class Response
        {
            public string Key { get; set; }

            public string Message { get; set; }
        }
    }
}