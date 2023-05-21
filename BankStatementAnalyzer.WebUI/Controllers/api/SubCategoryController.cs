using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
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
    public class SubCategoryController : ApiController
    {
        private readonly Lazy<ISubCategoryService> subcategoryService;
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<IColorsService> colorsService;
        private readonly Lazy<IHomeScreenLayoutService> homeScreenLayoutService;

        public SubCategoryController(Lazy<ISubCategoryService> subcategoryService,
            Lazy<ICategoryService> categoryService,
            Lazy<IColorsService> colorsService,
            Lazy<IHomeScreenLayoutService> homeScreenLayoutService)
        {
            this.subcategoryService = subcategoryService;
            this.categoryService = categoryService;
            this.colorsService = colorsService;
            this.homeScreenLayoutService = homeScreenLayoutService;
        }

        [HttpGet]
        [Route("api/SubCategory/Get/{isSpecialCategory:bool=false}")]
        public HttpResponseMessage GetAll(bool isSpecialCategory)
        {
            var categories = new List<Category>();
            IEnumerable<SubCategoryVM> subCategories;

            if (isSpecialCategory)
            {
                categories = this.categoryService.Value.FindBy(x => x.IsSpecialCategory == true).ToList();
                subCategories = GetCategories(categories);
            }
            else
            {
                categories = this.categoryService.Value.GetAll().ToList();
                subCategories = GetCategories(categories);
            }

            if (!subCategories.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
            }

            //IEnumerable<CategoryViewModel> modelList = from sub in subCategories
            //                                           join h in homeScreen on
            //                                           sub.Id equals h.Category.ID
            //                                           join cl in colors on
            //                                           h.Color.ID equals cl.ID
            //                                           orderby h.Row, h.Column
            //                                           select new CategoryViewModel()
            //                                           {
            //                                               Id = sub.Id,
            //                                               IconId = h.IconId,
            //                                               Name = sub.Name,
            //                                               IconColor = cl.Name,
            //                                               IsSpecialCategory = sub.IsSpecialCategory
            //                                           };

            return Request.CreateResponse(HttpStatusCode.OK, subCategories);
        }

        [HttpGet]
        [Route("api/SubCategory/Get")]
        public HttpResponseMessage GetSubCategory(int categoryId)
        {
            var subCategory = subcategoryService.Value.FindBy(x => x.CategoryID == categoryId).ToList();

            if (!subCategory.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
            }

            var model = from sub in subCategory
                        select new SubCategoryVM
                        {
                            Id = sub.ID,
                            Name = sub.Name,
                            CategoryID = sub.CategoryID,
                            Description = sub.Description,
                            Key = sub.Key,
                            ParentId = sub.ParentId,
                            Status = sub.Status
                        };

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        private IEnumerable<SubCategoryVM> GetCategories(List<Category> categories)
        {
            var categorySubCategoryModel = from cat in categories
                                           join sub in subcategoryService.Value.GetAll() on cat.ID equals sub.CategoryID
                                           select new SubCategoryVM()
                                           {
                                               Id = sub.ID,
                                               CategoryID = sub.CategoryID,
                                               Name = sub.Name,
                                               Key = sub.Key,
                                               Description = sub.Description,
                                               ParentId = sub.ParentId,
                                               Status = sub.Status,
                                               IsSpecialCategory = cat.IsSpecialCategory
                                           };

            return categorySubCategoryModel.ToList();

        }
    }
}
