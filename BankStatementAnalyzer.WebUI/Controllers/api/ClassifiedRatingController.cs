using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class ClassifiedRatingController : ApiController
    {
        private readonly IClassifiedRatingService classifiedRatingService;
        private readonly ISystemSettingService systemSettingService;

        public ClassifiedRatingController(IClassifiedRatingService classifiedRatingService,
            ISystemSettingService systemSettingService)
        {
            this.classifiedRatingService = classifiedRatingService;
            this.systemSettingService = systemSettingService;
        }

        [HttpPost]
        [Route("api/ClassifiedRating/SubmitReview/{Id:int=0}")]
        public HttpResponseMessage SubmitReview(ClassifiedRating model, int Id)
        {
            if (Convert.ToInt32(model.ClassifiedId) == 0)
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Classified ID can not be null." });

            //if (Convert.ToInt32(model.CategoryId) == 0)
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Category ID can not be null." });

            var outOfRating = systemSettingService.GetAll().Where(x => x.SettingTypeValue == "OutOfRating").FirstOrDefault();
            if (Convert.ToDouble(model.Rating) > Convert.ToDouble(outOfRating?.Value))
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Please provide valid rating. It should range from 1 to " +outOfRating?.Value });

            var record = classifiedRatingService.FindBy(x => x.ID == Id).FirstOrDefault();
            if (record != null)
            {
                record.Name = model.Name;
                record.CategoryId = model.CategoryId;
                record.ClassifiedId = model.ClassifiedId;
                record.Review = model.Review;
                record.Rating = model.Rating;
                record.CustomerId = model.CustomerId;

                classifiedRatingService.Edit(record);
                classifiedRatingService.Save();
                return Request.CreateResponse(HttpStatusCode.OK,  record.ID );
            }
            else
            {
                ClassifiedRating rating = new ClassifiedRating()
                {
                    CustomerId = Id,
                    ClassifiedId = model.ClassifiedId,
                    Rating = model.Rating,
                    Status = true,
                    Review = model.Review,
                    CategoryId = model.CategoryId
                };
                classifiedRatingService.Add(rating);
                classifiedRatingService.Save();
                return Request.CreateResponse(HttpStatusCode.OK, rating.ID );
            }
        }
    }
}
