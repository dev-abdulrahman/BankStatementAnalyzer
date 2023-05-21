using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.WebUI.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class ProductsController : ApiController
    {
        private readonly IProductService productService;
        private readonly ICompanyService companyService;

        public ProductsController(IProductService productService,
            ICompanyService companyService)
        {
            this.productService = productService;
            this.companyService = companyService;
        }

        [Route("api/Products/Get")]
        public HttpResponseMessage Get(string companyKey)
        {
            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Company details not found." });
            }

            var products = productService.GetAll().Where(x => x.Status == true).ToList();

            List<Products> response = new List<Products>();

            foreach (var product in products)
            {
                Products prdct = new Products
                {
                    Description = product.Description,
                    Id = product.ID,
                    Image = product.Files.Count() > 0 ? $"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/images/products/{product.Files.FirstOrDefault().Name}" : "",
                    Name = product.Name,
                    Price = product.Price,
                    Unit = product.UnitOfMeasure?.Name,
                    Size = string.Format("{0} {1}", product.Liters, product.UnitOfMeasure?.Name),
                    IsProductReturnable = product.IsReturnAble

                };

                if (!string.IsNullOrWhiteSpace(product.Description))
                {
                    IHtmlString str = new HtmlString(product.Description);
                    prdct.Description = str.ToString();
                }

                response.Add(prdct);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/Products/GetById")]
        public HttpResponseMessage GetById(int id)
        {
            var product = productService.FindBy(x => x.ID == id).FirstOrDefault();

            Products response = new Products
            {
                Description = product.Description,
                Id = product.ID,
                Image = product.Files.Count() > 0 ? $"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/images/products/{product.Files.FirstOrDefault().Name}" : "",
                Name = product.Name,
                Price = product.Price,
                Unit = product.UnitOfMeasure?.Name
            };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}