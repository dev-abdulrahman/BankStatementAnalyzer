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
    public class PushNotificationController : ApiController
    {
        private readonly IPushNotificationService pushNotificationService;
        private readonly ICustomerNotificationService customerNotificationService;

        public PushNotificationController(IPushNotificationService pushNotificationService, ICustomerNotificationService customerNotificationService)
        {
            this.pushNotificationService = pushNotificationService;
            this.customerNotificationService = customerNotificationService;
        }

        [HttpGet]
        [Route("api/Notification/{customerId:int=0}")]
        public HttpResponseMessage GetNotification(int customerId)
        {
            var notifications = pushNotificationService.GetAll().ToList();
            var isViewedByCustomer = customerNotificationService.FindBy(x => x.Customer.Id == customerId).ToList();

            if(!notifications.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
            }

            IEnumerable<PushNotificationViewModel> model = from notify in notifications
                                                           select new PushNotificationViewModel
                                                           {
                                                               Id = notify.ID,
                                                               ImageURL = notify.ImageURL,
                                                               Title = notify.Title,
                                                               Text = notify.Text,
                                                               ClassifiedId = notify.ClassifiedId,
                                                               IsViewed = (bool) isViewedByCustomer?.Where(x => x.NotificationId == notify.ID).Any()
                                                           };

            return Request.CreateResponse(HttpStatusCode.OK,  model);
        }

        [HttpPost]
        [Route("api/ViewNotification")]
        public HttpResponseMessage ViewNotification(CustomerNotification customerNotification)
        {
            if(customerNotification == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "Notification can not be null." });
            }

            if (customerNotification.IsViewed)
            {
                var isAlreadyViewed = customerNotificationService.FindBy(x => x.NotificationId == customerNotification.NotificationId && x.CustomerId == customerNotification.CustomerId).FirstOrDefault();
                if(isAlreadyViewed != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new List<string>());
                }
                else
                {
                    customerNotificationService.Add(customerNotification);
                }

                try
                {
                    customerNotificationService.Save();
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { result = false, message = "Failed to View Notification." });
                }

                return Request.CreateResponse(HttpStatusCode.OK, true );
            }

            return Request.CreateResponse(HttpStatusCode.OK,  true );
        }
    }
}