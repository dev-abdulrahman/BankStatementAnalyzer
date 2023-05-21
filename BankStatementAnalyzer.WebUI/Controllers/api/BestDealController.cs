using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    [AllowAnonymous]
    public class BestDealController : ApiController
    {
        private readonly Lazy<IBestDealService> bestDealService;
        private readonly Lazy<IClassifiedService> classifiedService;
        private readonly Lazy<ICategoryService> categoryService;
        private readonly Lazy<ICustomerService> customerService;

        public BestDealController(Lazy<IBestDealService> bestDealService,
            Lazy<IClassifiedService> classifiedService,
            Lazy<ICategoryService> categoryService,
            Lazy<ICustomerService> customerService)
        {
            this.bestDealService = bestDealService;
            this.classifiedService = classifiedService;
            this.categoryService = categoryService;
            this.customerService = customerService;
        }

        [HttpPost]
        [Route("api/BestDeal/Post")]
        public HttpResponseMessage Post(BestDeal bestDeal)
        {
            if(bestDeal.CustomerId <= 0)
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "CustomerId cannot be null." });

            if (bestDeal.ClassifiedId <= 0)
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "ClassifiedId cannot be null." });

            if (string.IsNullOrEmpty(bestDeal.Email))
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Email address cannot be null." });

            if(!customerService.Value.FindBy(x => x.Id == bestDeal.CustomerId).Any())
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>() );

            var classified = classifiedService.Value.FindBy(x => x.ID == bestDeal.ClassifiedId).FirstOrDefault();
            if (classified == null)
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());

            if(bestDeal.CategoryId > 0)
            {
                if(!categoryService.Value.FindBy(x => x.ID == bestDeal.CategoryId).Any())
                    return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
            }

            bestDealService.Value.Add(bestDeal);

            try
            {
                bestDealService.Value.Save();
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Failed to get the Best Deal." });
            }
            return Request.CreateResponse(HttpStatusCode.OK, "An email is sent to " + classified.Name +". They will contact to you soon.");
        }
    }
}
