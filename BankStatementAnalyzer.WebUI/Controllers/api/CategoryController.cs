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
    public class CategoryController : ApiController
    {
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<IColorsService> colorsService;
        private readonly Lazy<IHomeScreenLayoutService> homeScreenLayoutService;

        public CategoryController(Lazy<ICategoryService> categoryService,
            Lazy<IColorsService> colorsService,
            Lazy<IHomeScreenLayoutService> homeScreenLayoutService) 
        {
            this.categoryService = categoryService;
            this.colorsService = colorsService;
            this.homeScreenLayoutService = homeScreenLayoutService;
        }

        [HttpGet]
        [Route("api/Category/Get/{isSpecialCategory:bool=false}")]
        public HttpResponseMessage GetAll(bool isSpecialCategory)
        {
            var categories = new List<Category>();
            var colors = colorsService.Value.GetAll();
            var homeScreen = homeScreenLayoutService.Value.GetAll().ToList();

            if (isSpecialCategory)
            {
                categories = this.categoryService.Value.FindBy(x => x.IsSpecialCategory == true).ToList();
            }
            else
            {
                categories = this.categoryService.Value.GetAll().ToList();
            }

            if(!categories.Any())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new List<string>());
            }

            IEnumerable<CategoryViewModel> modelList = from c in categories
                                                       join h in homeScreen on
                                                       c.ID equals h.CategoryId
                                                       join cl in colors on
                                                       h.ColorId equals cl.ID
                                                       orderby h.Row, h.Column 
                                                       select new CategoryViewModel()
                                                       {
                                                           Id = c.ID,
                                                           IconId =  Convert.ToInt32(h.IconId),
                                                           Name = c.Name,
                                                           IconColor = cl.Name,
                                                           IsSpecialCategory = c.IsSpecialCategory
                                                       };
            
            return Request.CreateResponse(HttpStatusCode.OK, modelList);
        }
    }
}
