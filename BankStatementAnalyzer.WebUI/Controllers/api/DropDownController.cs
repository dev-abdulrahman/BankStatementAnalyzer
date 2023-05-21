using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.WebUI.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class DropDownController : ApiController
    {
        private readonly Lazy<IAreaManagerService> areaManagerService;
        private readonly Lazy<IStreetService> streetService;

        public DropDownController(Lazy<IAreaManagerService> areaManagerService,
            Lazy<IStreetService> streetService)
        {
            this.streetService = streetService;
            this.areaManagerService = areaManagerService;
        }

        [HttpGet]
        [Route("api/DropDown/GetPincode")]
        public HttpResponseMessage GetPincode()
        {
            var pincodes = areaManagerService.Value.GetAll().Select(x => x.Pincode).Distinct();

            return Request.CreateResponse(HttpStatusCode.OK, pincodes);
        }

        [HttpGet]
        [Route("api/DropDown/GetArea")]
        public HttpResponseMessage GetArea(int pincode)
        {
            var areas = areaManagerService.Value.FindBy(x => x.Pincode == pincode).ToList();

            List<Area> response = new List<Area>();
            foreach (var item in areas)
            {
                Area area = new Area
                {
                    Id = item.ID,
                    Name = item.Name
                };
                response.Add(area);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("api/DropDown/GetStreet")]
        public HttpResponseMessage GetStreet(int areaId)
        {
            var streets = streetService.Value.FindBy(x => x.AreaId == areaId).ToList();

            List<Street> response = new List<Street>();
            foreach (var item in streets)
            {
                Street street = new Street
                {
                    Id = item.ID,
                    Name = item.Name
                };
                response.Add(street);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
