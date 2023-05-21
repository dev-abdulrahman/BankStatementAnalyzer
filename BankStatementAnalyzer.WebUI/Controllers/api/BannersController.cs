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
    public class BannersController : ApiController
    {
        private readonly IBannersService bannersService;
        private readonly IBannerImagesService bannerImagesService;
        private readonly ICustomerLikesService customerLikesService;

        public BannersController(IBannersService bannersService,
            IBannerImagesService bannerImagesService,
            ICustomerLikesService customerLikesService)
        {
            this.bannersService = bannersService;
            this.bannerImagesService = bannerImagesService;
            this.customerLikesService = customerLikesService;
        }

        [HttpGet]
        [Route("api/Banners/Get/{customerId:int=0}")]
        public HttpResponseMessage GetAll(int customerId)
        {
            var banners = bannersService.GetAll().ToList();
            var bannerImages = bannerImagesService.GetAll().ToList();

            var customerLikes = customerLikesService.FindBy(x => x.CustomerId == customerId).ToList();

            List<BannersViewModel> modelList = new List<BannersViewModel>();
            if (!banners.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, modelList);
            }

            foreach(var banner in banners)
            {
                BannersViewModel model = new BannersViewModel();
                model.Id = banner.ID;
                model.Title = banner.Title;
                model.ShortDescription = banner.ShortDescription;
                model.LongDescription = banner.LongDescription;
                model.LikeCount = banner.LikeCount;
                model.Images = bannerImages.Where(x => x.BannerId == banner.ID).Select(s => s.ImageURL).ToList();
                model.IsLiked = (bool)customerLikes?.Where(x => x.BannerId == banner.ID).Any();
                modelList.Add(model);
            }

            return Request.CreateResponse(HttpStatusCode.OK,  modelList);
        }

        [HttpPost]
        [Route("api/BannerLikes")]
        public HttpResponseMessage BannerLikes(CustomerLikes customerLikes)
        {
            if (customerLikes == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
            }
            if (string.IsNullOrEmpty(customerLikes.CustomerId.ToString()) || string.IsNullOrEmpty(customerLikes.BannerId.ToString()))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
            }

            var bannerModel = bannersService.FindBy(x => x.ID == customerLikes.BannerId).FirstOrDefault();

            customerLikesService.CustomerLikeDislike(customerLikes, bannerModel);

            try
            {
                bannersService.Edit(bannerModel);
                customerLikesService.Save();
                bannersService.Save();
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Banner Like/Dislike failed." });
            }

            return Request.CreateResponse(HttpStatusCode.OK, bannerModel.LikeCount);
        }

    }
}
