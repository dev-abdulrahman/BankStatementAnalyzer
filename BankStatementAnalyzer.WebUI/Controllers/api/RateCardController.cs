using BankStatementAnalyzer.BusinessLogic.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class RateCardController : ApiController
    {
        private readonly IRateCardService rateCardService;
        private readonly ICategoryService categoryService;
        private readonly IServiceTypeService serviceTypeService;
        private readonly IClothsService clothsService;

        public RateCardController(IRateCardService rateCardService,
            ICategoryService categoryService,
            IServiceTypeService serviceTypeService,
            IClothsService clothsService)
        {
            this.rateCardService = rateCardService;
            this.categoryService = categoryService;
            this.serviceTypeService = serviceTypeService;
            this.clothsService = clothsService;
        }

        [HttpGet]
        [Route("api/RateCard/Get")]
        public HttpResponseMessage Get()
        {
            var rateCards = rateCardService.GetAll().Where(x => x.Status == true).GroupBy(x => x.Category).ToList();

            List<RateCard> response = new List<RateCard>();

            foreach (var rateCard in rateCards)
            {
                RateCard obj = new RateCard
                {
                    Name = rateCard.Key.Name,
                    Id = rateCard.Key.ID
                };

                foreach (var item in rateCard)
                {
                    if (obj.Services.Count() > 0 && obj.Services.Any(x => x.Id == item.ServiceTypeId))
                    {
                        Clothes clothes = new Clothes
                        {
                            Id = item.ClothId,
                            Name = item.Cloth?.Name,
                            NormalRate = item.NormalRate,
                            UrgentRate = item.UrgetRate
                        };

                        var srvc = obj.Services.Where(x => x.Id == item.ServiceTypeId).FirstOrDefault();

                        srvc.Clothes.Add(clothes);
                    }
                    else
                    {
                        Services service = new Services
                        {
                            Id = item.ServiceTypeId,
                            Name = item.ServiceType?.Name
                        };

                        Clothes clothes = new Clothes
                        {
                            Id = item.ClothId,
                            Name = item.Cloth?.Name,
                            NormalRate = item.NormalRate,
                            UrgentRate = item.UrgetRate
                        };

                        service.Clothes.Add(clothes);

                        obj.Services.Add(service);
                    }
                }

                response.Add(obj);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }

    public class RateCard
    {
        public RateCard()
        {
            Services = new List<Services>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Services> Services { get; set; }

    }

    public class Services
    {
        public Services()
        {
            Clothes = new List<Clothes>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Clothes> Clothes { get; set; }
    }

    public class Clothes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal UrgentRate { get; set; }

        public decimal NormalRate { get; set; }
    }
}