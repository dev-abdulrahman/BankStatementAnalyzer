using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using BankStatementAnalyzer.WebUI.Models;
using BankStatementAnalyzer.WebUI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    //[OutputCache(NoStore = true, Duration = 0)]
    public class PushNotificationController : BaseController
    {
        private readonly IPushNotificationService pushNotificationService;
        private readonly ICustomerService customerService;
        private readonly ISystemSettingService systemSettingService;
        private readonly ICompanyService companyService;
        private readonly IOrderService orderService;

        public PushNotificationController(ICustomerService customerService,
            IUserMasterService userMasterService, IPushNotificationService pushNotificationService,
            ILoggerFacade<BaseController> logger,
            ISystemSettingService systemSettingService,
            ICompanyService companyService,
            IOrderService orderService) : base(userMasterService, logger)
        {
            this.customerService = customerService;
            this.pushNotificationService = pushNotificationService;
            this.systemSettingService = systemSettingService;
            this.companyService = companyService;
            this.orderService = orderService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(pushNotificationService.GetAll().ToList());
        }

        // GET: PushNotification
        [HttpGet]
        public ActionResult SendNotification()
        {
            Load();

            PushNotificationDevices model = new PushNotificationDevices();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SendNotification(string message, PushNotificationDevices pushNotificationDevices)
        {

            if (pushNotificationDevices.UserType == UserType.AllUser)
                ModelState["DeviceIds"].Errors.Clear();

            if (!ModelState.IsValid)
            {
                Load();

                return View(pushNotificationDevices);
            }

            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();

            if (serverKey == null)
            {
                TempData["warning-message"] = "Server key missing kindly contact admin.";

                return RedirectToAction("Index");
            }

            var serverId = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerId).FirstOrDefault();

            if (serverId == null)
            {
                TempData["warning-message"] = "Server id missing kindly contact admin.";

                return RedirectToAction("Index");
            }

            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();

            if (pushBaseUrl == null)
            {
                TempData["warning-message"] = "Push notification url missing kindly contact admin.";

                return RedirectToAction("Index");
            }

            var pushGroupBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationGroupCreateURL).FirstOrDefault();

            if (pushGroupBaseUrl == null)
            {
                TempData["warning-message"] = "Push notification group create url missing kindly contact admin.";

                return RedirectToAction("Index");
            }

            var customers = new List<Customer>();
            var deviceIds = new List<string>();
            var customerNames = new List<string>();


            customers = (from cu in customerService.GetAll()
                         where cu.DeviceId != null & cu.DeviceId != "0" & cu.DeviceId != string.Empty
                         select cu).ToList();

            NotificationKeyModel notificationKeyModel;

            if (pushNotificationDevices.UserType == UserType.AllUser)
            {
                foreach (var customer in customers)
                {
                    deviceIds.Add(customer.DeviceId);
                    customerNames.Add(customer.Username);
                }
                notificationKeyModel = await GetNotificationKeyAsync(deviceIds, serverKey.Value, serverId.Value, pushGroupBaseUrl.Value);

            }
            else
            {
                foreach (var deviceId in pushNotificationDevices.DeviceIds)
                {
                    if (!string.IsNullOrWhiteSpace(deviceId))
                    {
                        foreach (var customer in customers.Where(x => x.DeviceId == deviceId))
                        {
                            customerNames.Add(customer.Username);
                        }
                    }
                }

                notificationKeyModel = await GetNotificationKeyAsync(pushNotificationDevices.DeviceIds, serverKey.Value, serverId.Value, pushGroupBaseUrl.Value);
            }

            if (notificationKeyModel == null)
            {
                TempData["warning-message"] = "Push notification key generation failed.";

                return RedirectToAction("Index");
            }

            var result = await pushNotificationService.Send(pushNotificationDevices.Title, pushNotificationDevices.Text, notificationKeyModel.notification_key, serverKey.Value, pushBaseUrl.Value);

            if (result)
            {
                foreach (var customer in customerNames)
                {
                    PushNotification pushNotification = new PushNotification
                    {
                        Status = true,
                        Title = pushNotificationDevices.Title,
                        Text = pushNotificationDevices.Text,
                        CreatedBy = UserName
                    };

                    pushNotificationService.Add(pushNotification);
                    pushNotificationService.Save();
                }

                TempData["success-message"] = "Push notification sent successfully";

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var customer in customerNames)
                {
                    PushNotification pushNotification = new PushNotification
                    {
                        Status = false,
                        Title = pushNotificationDevices.Title,
                        Text = pushNotificationDevices.Text,
                        CreatedBy = UserName
                    };

                    pushNotificationService.Add(pushNotification);
                    pushNotificationService.Save();
                }

                TempData["error-message"] = "Push notification sending failed";

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Reminder()
        {
            PushNotificationDevices model = new PushNotificationDevices();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Reminder(string message, PushNotificationDevices pushNotificationDevices)
        {
            if (pushNotificationDevices.UserType == UserType.AllUser)
                ModelState["DeviceIds"].Errors.Clear();

            if (!ModelState.IsValid)
            {
                return View(pushNotificationDevices);
            }

            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();

            if (serverKey == null)
            {
                TempData["warning-message"] = "Server key missing kindly contact admin.";

                return RedirectToAction("Reminder");
            }

            var serverId = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerId).FirstOrDefault();

            if (serverId == null)
            {
                TempData["warning-message"] = "Server id missing kindly contact admin.";

                return RedirectToAction("Reminder");
            }

            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();

            if (pushBaseUrl == null)
            {
                TempData["warning-message"] = "Reminder url missing kindly contact admin.";

                return RedirectToAction("Reminder");
            }

            var pushGroupBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationGroupCreateURL).FirstOrDefault();

            if (pushGroupBaseUrl == null)
            {
                TempData["warning-message"] = "Reminder group create url missing kindly contact admin.";

                return RedirectToAction("Reminder");
            }

            var deviceIds = new List<string>();
            var customerNames = new List<string>();

            NotificationKeyModel notificationKeyModel;

            if (pushNotificationDevices.UserType == UserType.AllUser)
            {
                var orders = orderService.FindBy(x => x.OrderStatus == OrderStatus.Delivered && x.DeliveryDate.Value >= pushNotificationDevices.FromDate && x.DeliveryDate <= pushNotificationDevices.ToDate).ToList();

                List<SelectListItem> listItems = new List<SelectListItem>();

                foreach (var order in orders)
                {
                    if (!string.IsNullOrWhiteSpace(order.Customer?.DeviceId))
                    {
                        if (!customerNames.Any(x => x.Contains(order.Customer.Username)))
                        {
                            deviceIds.Add(order.Customer.DeviceId);
                            customerNames.Add(order.Customer.Username);
                        }
                    }
                }
                notificationKeyModel = await GetNotificationKeyAsync(deviceIds, serverKey.Value, serverId.Value, pushGroupBaseUrl.Value);
            }
            else
            {
                foreach (var deviceId in pushNotificationDevices.DeviceIds)
                {
                    if (!string.IsNullOrWhiteSpace(deviceId))
                    {
                        var customer = customerService.FindBy(x => x.DeviceId == deviceId).FirstOrDefault();
                        if (!customerNames.Any(x => x.Contains(customer.Username)))
                        {
                            customerNames.Add(customer.Username);
                        }
                    }
                }

                notificationKeyModel = await GetNotificationKeyAsync(pushNotificationDevices.DeviceIds, serverKey.Value, serverId.Value, pushGroupBaseUrl.Value);
            }

            if (notificationKeyModel == null)
            {
                TempData["warning-message"] = "Reminder key generation failed.";

                return RedirectToAction("Reminder");
            }

            var result = await pushNotificationService.Send(pushNotificationDevices.Title, pushNotificationDevices.Text, notificationKeyModel.notification_key, serverKey.Value, pushBaseUrl.Value);

            if (result)
            {
                foreach (var customer in customerNames)
                {
                    PushNotification pushNotification = new PushNotification
                    {
                        Status = true,
                        Title = pushNotificationDevices.Title,
                        Text = pushNotificationDevices.Text,
                        CreatedBy = UserName
                    };

                    pushNotificationService.Add(pushNotification);
                    pushNotificationService.Save();
                }

                TempData["success-message"] = "Reminder sent successfully";

                return RedirectToAction("Reminder");
            }
            else
            {
                foreach (var customer in customerNames)
                {
                    PushNotification pushNotification = new PushNotification
                    {
                        Status = false,
                        Title = pushNotificationDevices.Title,
                        Text = pushNotificationDevices.Text,
                        CreatedBy = UserName
                    };

                    pushNotificationService.Add(pushNotification);
                    pushNotificationService.Save();
                }

                TempData["error-message"] = "Reminder sending failed";

                return RedirectToAction("Reminder");
            }
        }

        [AllowAnonymous]
        public JsonResult GetCustomer(DateTime fromDate, DateTime toDate)
        {
            var orders = orderService.FindBy(x => x.OrderStatus == OrderStatus.Delivered && x.DeliveryDate.Value >= fromDate && x.DeliveryDate <= toDate).ToList();

            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var order in orders)
            {
                if (!string.IsNullOrWhiteSpace(order.Customer?.DeviceId))
                {
                    if (!listItems.Any(x => x.Text == order.Customer.Username))
                    {
                        listItems.Add(new SelectListItem { Value = order.Customer.DeviceId, Text = order.Customer.Username });
                    }
                }
            }

            return Json(new { result = listItems }, JsonRequestBehavior.AllowGet);
        }
        private async Task<NotificationKeyModel> GetNotificationKeyAsync(List<string> deviceIds, string serverKey, string serverId, string pushGroupBaseUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var data = new
                {
                    operation = "create",
                    notification_key_name = $"allUsers_{Guid.NewGuid()}",
                    registration_ids = deviceIds
                };

                var pushUrlForNotificationKey = $"{pushGroupBaseUrl}";

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={serverKey}");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("project_id", $"{serverId}");

                HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync($"{pushUrlForNotificationKey}", data);

                var response = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<NotificationKeyModel>(response);
                }
                else
                {
                    return null;
                }

            }
        }

        private void Load()
        {
            ViewBag.customers = customerService.GetAll().Where(x => x.DeviceId != null && x.DeviceId != "0" && x.DeviceId != string.Empty).Select(x => new SelectListItem { Value = x.DeviceId, Text = x.Username }).ToList();
        }
    }
}