using BankStatementAnalyzer.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class ClothsController : ApiController
    {
        private readonly IClothsService clothsService;

        public ClothsController(IClothsService clothsService)
        {
            this.clothsService = clothsService;
        }

        [HttpGet]
        [Route("api/Cloths/Get")]
        public HttpResponseMessage Get()
        {
            var cloths = clothsService.GetAll().Include("Category").Where(x => x.Status == true).ToList();

            List<Cloths> response = new List<Cloths>();
            foreach (var cloth in cloths)
            {
                Cloths clth = new Cloths
                {
                    Id = cloth.ID,
                    Name = $"{cloth.Category?.Name} - {cloth.Name}"
                };

                response.Add(clth);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }

    public class Cloths
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
