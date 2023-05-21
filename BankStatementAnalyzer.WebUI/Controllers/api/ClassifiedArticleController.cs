using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.WebUI.ViewModels;
using BankStatementAnalyzer.WebUI.ViewModels.Api;
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
    public class ClassifiedArticleController : ApiController
    {
        private readonly ISubCategoryService subCategoryService;
        private readonly IClassifiedService classifiedService;
        private readonly IClassifiedArticleService classifiedArticleService;
        private readonly IClassifiedArticleImagesService classifiedArticleImagesService;
        private readonly IClassifiedArticleKeywordsService classifiedArticleKeywordsService;
        private readonly IClassifiedArticleCategoryService classifiedArticleCategoryService;

        public ClassifiedArticleController(ISubCategoryService subCategoryService,
            IClassifiedService classifiedService,
            IClassifiedArticleService classifiedArticleService,
            IClassifiedArticleImagesService classifiedArticleImagesService,
            IClassifiedArticleKeywordsService classifiedArticleKeywordsService,
            IClassifiedArticleCategoryService classifiedArticleCategoryService)
        {
            this.subCategoryService = subCategoryService;
            this.classifiedService = classifiedService;
            this.classifiedArticleService = classifiedArticleService;
            this.classifiedArticleImagesService = classifiedArticleImagesService;
            this.classifiedArticleKeywordsService = classifiedArticleKeywordsService;
            this.classifiedArticleCategoryService = classifiedArticleCategoryService;
        }

        [HttpGet]
        [Route("api/Article/ByCategory")]
        public HttpResponseMessage GetArticleByCategory(int SubCategoryId)
        {
            var articleList = classifiedArticleCategoryService.FindBy(x => x.CategoryId == SubCategoryId).ToList();

            if (!articleList.Any())
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());

            var model = GetAllArticles(articleList.Select(s => s.ClassifiedArticleId).ToList());

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("api/Article/ByName")]
        public HttpResponseMessage GetArticleByName(string SearchName)
        {
            var articleIdList = classifiedArticleService.FindBy(x => x.Name.Contains(SearchName)).Select(s => s.ID).Distinct().ToList();
            var articleKeywordsList = classifiedArticleKeywordsService.FindBy(x => x.Keywords.Contains(SearchName)).Select(s => s.ClassifiedArticleId).Distinct().ToList();

            articleIdList.AddRange(articleKeywordsList);

            if (!articleIdList.Distinct().Any())
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());

            var model = GetAllArticles(articleIdList);

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("api/Article/ByArticleId")]
        public HttpResponseMessage GetArticleByArticleId(int articleId)
        {
            var articleList = classifiedArticleService.FindBy(x => x.ID == articleId).ToList();

            if (!articleList.Any())
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());

            var model = GetAllArticles(articleList.Select(s => s.ID).ToList());

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        private List<ClassifiedArticleApiVM> GetAllArticles(List<int> articleIdList)
        {
            List<ClassifiedArticleApiVM> articleApiViewModel = new List<ClassifiedArticleApiVM>();
            //var allCategories = categoryService.GetAll();
            var allSubcategories = subCategoryService.GetAll();

            var filePath = ConfigurationManager.AppSettings["UploadArticleImage"];
            filePath = filePath.Replace(@"\", "/");

            foreach (var articleId in articleIdList.Distinct())
            {
                ClassifiedArticleApiVM model = new ClassifiedArticleApiVM();
                var classified = classifiedArticleService.FindBy(x => x.ID == articleId).FirstOrDefault();
                if (classified != null)
                {
                    model.Id = classified.ID;
                    model.Name = classified.Name;
                    model.Address = classified.Address;
                    model.Heading = classified.Heading;
                    model.SubHeading = classified.SubHeading;
                    model.ContactNo = classified.ContactNo;
                    model.Availability = ((ArticleAvailable) Convert.ToInt32(classified.Status)).ToString();
                    model.ShortDescription = classified.ShortDescription;
                    model.ArticleUrl = "/ClassifiedArticle/GetArticle?articleId=" + classified.ID;
                }

                var categories = classifiedArticleCategoryService.FindBy(x => x.ClassifiedArticleId == articleId).ToList();
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

                var classifiedImages = classifiedArticleImagesService.FindBy(x => x.ClassifiedArticleId == articleId).ToList();
                foreach (var image in classifiedImages)
                {
                    model.Images.Add(filePath + image.Image);
                }

                articleApiViewModel.Add(model);
            }
            return articleApiViewModel;
        }
    }
}
