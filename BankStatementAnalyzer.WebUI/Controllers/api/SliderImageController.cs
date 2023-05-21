using BankStatementAnalyzer.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class SliderImageController : ApiController
    {
        private readonly IGalleryService galleryService;
        private readonly IImageCategoryService imageCategoryService;
        private readonly ICompanyService companyService;

        public SliderImageController(IGalleryService galleryService,
            IImageCategoryService imageCategoryService,
            ICompanyService companyService)
        {
            this.galleryService = galleryService;
            this.imageCategoryService = imageCategoryService;
            this.companyService = companyService;
        }

        [HttpGet]
        [Route("api/SilderImage/Get")]
        public HttpResponseMessage Get(string companyKey)
        {
            var value = Convert.ToInt16(ConfigurationManager.AppSettings["sliderImageCount"]);
            var imageCategory = ConfigurationManager.AppSettings["imageCategory"];

            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Company details not found." });
            }

            var dbImageCategory = imageCategoryService.FindBy(x => x.Name == imageCategory).FirstOrDefault();

            if (dbImageCategory != null)
            {
                var images = galleryService.FindBy(x => x.ImageCategoryId == dbImageCategory.ID).ToList();

                List<string> strings = new List<string>();

                foreach (var image in images)
                {
                    strings.Add($"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/images/gallery/{image.File.Name}");
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { result = true, strings });
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "No slider image found" });
        }
    }
}
