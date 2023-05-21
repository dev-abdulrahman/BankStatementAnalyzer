using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class ClassifiedController : ApiController
    {
        private readonly ICategoryService categoryService;
        private readonly IClassifiedService classifiedService;
        private readonly IClassifiedImagesService classifiedImagesService;
        private readonly IClassifiedCategoryService classifiedCategoryService;
        private readonly ISystemSettingService systemSettingService;
        private readonly IClassifiedKeywordsService classifiedKeywordsService;
        private readonly IClassifiedContactsService classifiedContactsService;
        private readonly IClassifiedRatingService classifiedRatingService;
        private readonly ISubCategoryService subcategoryService;
        private readonly ICustomerService customerService;

        public ClassifiedController(ICategoryService categoryService,
            IClassifiedService classifiedService,
            IClassifiedImagesService classifiedImagesService,
            IClassifiedCategoryService classifiedCategoryService,
            ISystemSettingService systemSettingService,
            IClassifiedKeywordsService classifiedKeywordsService,
            IClassifiedContactsService classifiedContactsService,
            IClassifiedRatingService classifiedRatingService,
            ISubCategoryService subcategoryService,
            ICustomerService customerService)
        {
            this.categoryService = categoryService;
            this.classifiedService = classifiedService;
            this.classifiedImagesService = classifiedImagesService;
            this.classifiedCategoryService = classifiedCategoryService;
            this.systemSettingService = systemSettingService;
            this.systemSettingService = systemSettingService;
            this.classifiedKeywordsService = classifiedKeywordsService;
            this.classifiedContactsService = classifiedContactsService;
            this.classifiedRatingService = classifiedRatingService;
            this.subcategoryService = subcategoryService;
            this.customerService = customerService;
        }

        [HttpGet]
        [Route("api/Classified/ByCategory")]
        public HttpResponseMessage GetClassifiedByCategory(int SubCategoryId, double Lat, double Long)
        {
            var classifiedList = classifiedCategoryService.FindBy(x => x.CategoryId == SubCategoryId).ToList();

            if (!classifiedList.Any())
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());

            var model = GetAllClassified(classifiedList.Select(s => s.ClassifiedId).ToList(), Lat, Long);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("api/Classified/ByName")]
        public HttpResponseMessage GetClassifiedByName(string SearchName, double Lat, double Long)
        {
            var classifiedIdList = classifiedService.FindBy(x => x.Name.Contains(SearchName)).Select(s => s.ID).Distinct().ToList();
            var classifiedKeywordsList = classifiedKeywordsService.FindBy(x => x.Keyword.Contains(SearchName)).Select(s => s.ClassifiedId).Distinct().ToList();

            classifiedIdList.AddRange(classifiedKeywordsList);

            if (!classifiedIdList.Distinct().Any())
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());

            var model = GetAllClassified(classifiedIdList, Lat, Long);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("api/Classified/ByClassifiedId")]
        public HttpResponseMessage GetClassifiedByClassifiedId(int classifiedId, double Lat, double Long)
        {
            var classifiedList = classifiedService.FindBy(x => x.ID == classifiedId).ToList();

            if (!classifiedList.Any())
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());

            var model = GetAllClassified(classifiedList.Select(s => s.ID).ToList(), Lat, Long);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        private List<ClassifiedApiViewModel> GetAllClassified(List<int> classifiedIdList, double latitude, double longitude)
        {
            List<ClassifiedApiViewModel> classifiedApiViewModel = new List<ClassifiedApiViewModel>();
            //var allCategories = categoryService.GetAll();
            var allSubcategories = subcategoryService.GetAll();

            var outOfRating = systemSettingService.GetAll().Where(x => x.SettingTypeValue == "OutOfRating").FirstOrDefault();
            var filePath = ConfigurationManager.AppSettings["UploadClassifiedImage"];
            filePath = filePath.Replace(@"\", "/");

            foreach (var classifiedId in classifiedIdList.Distinct())
            {
                ClassifiedApiViewModel model = new ClassifiedApiViewModel();
                var classified = classifiedService.FindBy(x => x.ID == classifiedId).FirstOrDefault();
                if (classified != null)
                {
                    model.Id = classified.ID;
                    model.Name = classified.Name;
                    model.Address1 = classified.Address1;
                    model.Address2 = classified.Address2;
                    model.Latitude = classified.Latitude;
                    model.Longitude = classified.Longitude;
                    model.Availability = ((AvailabilityType) classified.Availability).ToString();
                    model.Description = classified.Description;
                    model.SpecialistIn = classified.SpecialistIn;
                    model.Location = classified.Location;
                    var distance = GeoLocation.GetDistanceBetweenPoints(latitude, longitude, classified.Latitude, classified.Longitude);
                    model.Distance = distance.ToString() == "NaN" ? 0.0 : Convert.ToDouble(distance);
                }

                var categories = classifiedCategoryService.FindBy(x => x.ClassifiedId == classifiedId).ToList();
                if (categories.Any())
                {
                    foreach (var cat in categories)
                    {
                        var category = allSubcategories.Where(x => x.ID == cat.CategoryId).FirstOrDefault();
                        if (category != null)
                        {
                            model.Categories.Add(category.Name);
                        }
                    }
                }

                var classifiedImages = classifiedImagesService.FindBy(x => x.ClassifiedId == classifiedId).ToList();
                foreach (var image in classifiedImages)
                {
                    model.Images.Add(filePath + image.ImageUrl);
                }

                var contacts = classifiedContactsService.FindBy(x => x.ClassifiedId == classifiedId).ToList();
                if (contacts != null && contacts.Any())
                {
                    model.ContactNo = contacts.Select(x => x.ContactNo).FirstOrDefault();
                    if (contacts.Count() > 1)
                        model.OptionalContactNo = contacts.Select(x => x.ContactNo).LastOrDefault();
                }

                var reviews = classifiedRatingService.FindBy(x => x.ClassifiedId == classifiedId).ToList();
                var customerIdList = reviews?.Select(x => x.ClassifiedId).ToList();
                var customers = customerService.FindBy(x => customerIdList.Contains(x.Id)).ToList();
                foreach (var review in reviews)
                {
                    model.Reviews.Add(new ClassifiedRatingVM()
                    {
                        Id = review.ID,
                        Name = customers?.Where(x => x.Id == review.CustomerId).FirstOrDefault()?.Username,
                        Review = review.Review,
                        Rating = review.Rating,
                        ClassifiedId = review.ClassifiedId,
                        CategoryId = review.CategoryId
                    });
                }

                model.TotalRatings = Convert.ToInt32(outOfRating.Value);
                if (model.Reviews.Any())
                {
                    model.Rating = model.Reviews.Select(x => x.Rating).Any() ? model.Reviews.Select(x => x.Rating).Average() : 0;
                    model.Rating = Math.Round(model.Rating, 2);
                }
                classifiedApiViewModel.Add(model);
            }
            return classifiedApiViewModel.OrderBy<ClassifiedApiViewModel, double>((Func<ClassifiedApiViewModel, double>)(x => x.Distance)).ToList();
        }
    }
}
