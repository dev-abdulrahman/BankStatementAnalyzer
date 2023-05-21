using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.WebUI.Filters;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class ServiceAvailibilityController : ApiController
    {
        private readonly IStoreService storeService;
        private readonly ICompanyService companyService;
        private readonly IDistanceService distanceService;

        public ServiceAvailibilityController(IStoreService storeService,
            ICompanyService companyService,
            IDistanceService distanceService)
        {
            this.storeService = storeService;
            this.companyService = companyService;
            this.distanceService = distanceService;
        }

        [HttpGet]
        [Route("api/ServiceAvailibility/Get")]
        [CacheFilter(TimeDuration = 60)]
        public HttpResponseMessage Get(double lat, double lng)
        {
            var result = distanceService.GetService(lat, lng);

            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = true });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result = false });
        }

        //[HttpGet]
        //[Route("api/Store/Get")]
        //[CacheFilter(TimeDuration = 60)]
        //public HttpResponseMessage Get(string companyKey, double lat, double longitude, string pincode)
        //{
        //    var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

        //    if (company == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Company details not found." });
        //    }

        //    var stores = storeService.FindBy(x => x.Pincode == pincode);

        //    var result = distanceService.GetServiceAvailability(stores, lat, longitude);

        //    if (result)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, new { result = true });
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, new { result = false });
        //}

        [HttpGet]
        [Route("api/Store/ManualCheck")]
        [CacheFilter(TimeDuration = 60)]
        public HttpResponseMessage ManualCheck(string pincode, string companyKey)
        {
            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Company details not found." });
            }

            var stores = storeService.FindBy(x => x.Pincode == pincode);

            if (stores.Count() > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = true });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result = false });
        }

        [HttpGet]
        [Route("api/Store/InstaService")]
        [CacheFilter(TimeDuration = 60)]
        public HttpResponseMessage InstaService(string companyKey, double lat, double longitude, string pincode)
        {
            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Company details not found." });
            }

            var stores = storeService.FindBy(x => x.Pincode == pincode && x.IsInstaServiceProvided == true);

            var result = distanceService.GetServiceAvailability(stores, lat, longitude);

            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = true });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result = false });
        }
    }
}