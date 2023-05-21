using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.WebUI.ViewModels;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class CarouselController : ApiController
    {
        private readonly IClassifiedService classifiedService;
        private readonly ICarouselService carouselService;

        public CarouselController(IClassifiedService classifiedService,
            ICarouselService carouselService)
        {
            this.classifiedService = classifiedService;
            this.carouselService = carouselService;
        }

        [HttpGet]
        [Route("api/ImageCarousel/Get/{classifiedId:int=0}")]
        public HttpResponseMessage Get(int classifiedId)
        {
            var filePath = ConfigurationManager.AppSettings["UploadCarouselImage"];
            filePath = filePath.Replace(@"\", "/");

            var carousel = new List<Carousel>();
            if (classifiedId > 0)
                carousel = carouselService.FindBy(x => x.ClassifiedId == classifiedId).ToList();
            else
                carousel = carouselService.GetAll().ToList();

            foreach (var item in carousel)
            {
                item.ImageUrl = filePath + item.ImageUrl;
            }

            var model = from car in carousel
                        select new CarouselApiVM()
                        {
                            Id = car.ID,
                            ClassifiedId = car.ClassifiedId,
                            ImageUrl = car.ImageUrl,
                            Status = car.Status
                        };

            return Request.CreateResponse(HttpStatusCode.OK, model );
        }
    }
}
