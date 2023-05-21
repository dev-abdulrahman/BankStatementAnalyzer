using BankStatementAnalyzer.BusinessLogic.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class ServiceTypeController : ApiController
    {
        private readonly IServiceTypeService serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            this.serviceTypeService = serviceTypeService;
        }

        [HttpGet]
        [Route("api/ServiceType/Get")]
        public HttpResponseMessage Get()
        {
            var serviceTypes = serviceTypeService.GetAll().Where(x => x.Status == true);

            List<ServiceType> response = new List<ServiceType>();

            foreach (var serviceType in serviceTypes)
            {
                ServiceType stype = new ServiceType
                {
                    Id = serviceType.ID,
                    Name = serviceType.Name
                };

                response.Add(stype);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }

    public class ServiceType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}