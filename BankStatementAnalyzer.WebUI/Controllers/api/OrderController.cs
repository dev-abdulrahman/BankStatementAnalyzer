using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BankStatementAnalyzer.WebUI.Controllers.api
{
    public class OrderController : ApiController
    {
        private readonly ICustomerService customerService;
        private readonly IOrderService orderService;
        private readonly IAddressService addressService;
        private readonly IOrderTypeService orderTypeService;
        private readonly IOrderDetailService orderDetailService;
        private readonly IOrderStatusMappingService orderStatusMappingService;
        private readonly IManageOrderService manageOrderService;
        private readonly ISystemSettingService systemSettingService;
        private readonly ILoggerFacade<OrderController> logger;
        private readonly ISendEmailService sendEmailService;

        private readonly string linqTake;

        public OrderController(
            IOrderService orderService,
            ICustomerService customerService,
            IAddressService addressService,
            IOrderTypeService orderTypeService,
            IOrderDetailService orderDetailService,
            IOrderStatusMappingService orderStatusMappingService,
            IManageOrderService manageOrderService,
            ISystemSettingService systemSettingService,
            ILoggerFacade<OrderController> logger,
            ISendEmailService sendEmailService)
        {
            this.customerService = customerService;
            this.orderService = orderService;
            this.addressService = addressService;
            this.orderTypeService = orderTypeService;
            this.orderDetailService = orderDetailService;
            this.orderStatusMappingService = orderStatusMappingService;
            this.manageOrderService = manageOrderService;
            this.systemSettingService = systemSettingService;
            this.logger = logger;
            this.sendEmailService = sendEmailService;

            linqTake = ConfigurationManager.AppSettings["linqTake"];
        }

        [HttpGet]
        [Route("api/Order/Type")]
        public HttpResponseMessage Type()
        {
            var orderType = orderTypeService.GetAll().Where(x => x.Status == true);

            List<OrderType> otype = new List<OrderType>();
            foreach (var item in orderType)
            {
                OrderType type = new OrderType { Id = item.ID, Name = item.Name };

                otype.Add(type);
            }
            return Request.CreateResponse(HttpStatusCode.OK, otype);
        }

        [HttpPost]
        [Route("api/Order/Create")]
        public async Task<HttpResponseMessage> Create(OrderCreate orderCreate)
        {
            var customer = customerService.FindBy(x => x.UID == orderCreate.CustomerId).FirstOrDefault();
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            if (orderCreate.AddressId > 0)
            {
                var address = addressService.FindBy(x => x.ID == orderCreate.AddressId).FirstOrDefault();

                if (address == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Address not found." });
                }
            }

            var orderType = orderTypeService.FindBy(x => x.ID == orderCreate.OrderTypeId).FirstOrDefault();
            if (orderType == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Order type not found." });
            }

            BankStatementAnalyzer.Models.Order order = new BankStatementAnalyzer.Models.Order
            {
                OrderStatus = OrderStatus.Ordered,
                OrderTypeId = orderCreate.OrderTypeId,
                CustomerID = customer.Id,
                AddressID = orderCreate.AddressId > 0 ? orderCreate.AddressId : null,
                PackageId = orderCreate.PackageId > 0 ? orderCreate.PackageId : null,
                CouponId = orderCreate.CouponId > 0 ? orderCreate.CouponId : null,
                IsUrgent = orderCreate.IsUrgent,
                PaidAmount = orderCreate.PackageAmount,
                UpdatedDate = DateTime.Now.Date
            };

            orderService.Add(order);
            orderService.Save();

            #region Create Status
            OrderStatusMapping ordered = new OrderStatusMapping
            {
                OrderId = order.ID,
                OrderStatus = OrderStatus.Ordered,
                CreatedOn = DateTime.Now,
                EstimatedDate = DateTime.Now
            };

            orderStatusMappingService.Add(ordered);

            OrderStatusMapping assignedForPickUp = new OrderStatusMapping
            {
                OrderId = order.ID,
                OrderStatus = OrderStatus.AssignedForPickUp,
                CreatedOn = DateTime.Now,
                EstimatedDate = null
            };

            orderStatusMappingService.Add(assignedForPickUp);

            OrderStatusMapping pickedUp = new OrderStatusMapping
            {
                OrderId = order.ID,
                OrderStatus = OrderStatus.PickedUp,
                CreatedOn = DateTime.Now,
                EstimatedDate = null
            };

            orderStatusMappingService.Add(pickedUp);

            OrderStatusMapping UnderProcessing = new OrderStatusMapping
            {
                OrderId = order.ID,
                OrderStatus = OrderStatus.UnderProcessing,
                CreatedOn = DateTime.Now,
                EstimatedDate = null
            };

            orderStatusMappingService.Add(UnderProcessing);

            OrderStatusMapping ReadyForDelivery = new OrderStatusMapping
            {
                OrderId = order.ID,
                OrderStatus = OrderStatus.ReadyForDelivery,
                CreatedOn = DateTime.Now,
                EstimatedDate = null
            };

            orderStatusMappingService.Add(ReadyForDelivery);

            OrderStatusMapping AssignedForDelivery = new OrderStatusMapping
            {
                OrderId = order.ID,
                OrderStatus = OrderStatus.AssignedForDelivery,
                CreatedOn = DateTime.Now,
                EstimatedDate = null
            };

            orderStatusMappingService.Add(AssignedForDelivery);

            OrderStatusMapping Delivered = new OrderStatusMapping
            {
                OrderId = order.ID,
                OrderStatus = OrderStatus.Delivered,
                CreatedOn = DateTime.Now,
                EstimatedDate = null
            };

            orderStatusMappingService.Add(Delivered);
            orderStatusMappingService.Save();
            #endregion


            await sendAdminEmail(customer, order);

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Order Placed Successfully.", OrderId = order.ID });
        }

        [HttpPost]
        [Route("api/Order/List")]
        public async Task<HttpResponseMessage> List(List<OrderList> orderLists)
        {
            foreach (var orderList in orderLists)
            {
                if (orderList.OrderID == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Order id cannot be 0." });
                }

                BankStatementAnalyzer.Models.OrderDetail orderDetail = new BankStatementAnalyzer.Models.OrderDetail
                {
                    OrderID = orderList.OrderID,
                    ClothsID = orderList.ClothsID,
                    ServiceTypeID = orderList.ServiceTypeID,
                    IsUrgent = orderList.IsUrgent,
                    Quantity = orderList.Quantity,
                    Price = orderList.Price,
                    TotalPayable = orderList.TotalPayable,
                    OrderStatus = OrderStatus.Ordered
                };

                orderDetailService.Add(orderDetail);
                orderDetailService.Save();
            }

            if (orderLists.Count() > 0)
            {
                var orderId = orderLists.FirstOrDefault().OrderID;
                var order = orderService.FindBy(x => x.ID == orderId).FirstOrDefault();
                order.IsUrgent = orderLists.Any(x => x.IsUrgent == true);
                orderService.Edit(order);
                orderService.Save();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Order List Item Created Successfully." });
        }

        [HttpPost]
        [Route("api/Order/Cancel")]
        public HttpResponseMessage Cancel(OrderCancel OrderCancel)
        {
            var customer = customerService.FindBy(x => x.UID == OrderCancel.CustomerId).FirstOrDefault();
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            var order = orderService.FindBy(x => x.ID == OrderCancel.OrderId).FirstOrDefault();

            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid order details." });
            }

            if (order.OrderStatus == OrderStatus.Ordered)
            {
                order.OrderStatus = OrderStatus.Cancelled;
                order.UpdatedBy = customer.Email;
                order.UpdatedDate = DateTime.Now.Date;

                orderService.Edit(order);
                orderService.Save();

                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Order cancelled." });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Order can not be cancelled." });
        }

        [HttpPost]
        [Route("api/Order/CancelListItem")]
        public HttpResponseMessage CancelListItem(OrderCancel cancel)
        {
            var customer = customerService.FindBy(x => x.UID == cancel.CustomerId).FirstOrDefault();
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            var orderDetail = orderDetailService.FindBy(x => x.Id == cancel.OrderId).FirstOrDefault();

            if (orderDetail == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid order item details." });
            }

            if (orderDetail.OrderStatus == OrderStatus.Ordered)
            {
                orderDetail.OrderStatus = OrderStatus.Cancelled;

                orderDetailService.Edit(orderDetail);
                orderDetailService.Save();

                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Order item cancelled." });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Order item can not be cancelled." });
        }


        [HttpGet]
        [Route("api/Order/Get")]
        public HttpResponseMessage Get(string customerUId)
        {
            var user = customerService.FindBy(x => x.UID == customerUId).FirstOrDefault();

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            var response = manageOrderService.GetTop20Orders(user.Id);

            return Request.CreateResponse(HttpStatusCode.OK, new { CurrentOrder = response.Item2, PastOrder = response.Item1 });
        }

        [HttpGet]
        [Route("api/Order/GetByOrderId")]
        public HttpResponseMessage GetByOrderId(int orderId, string customerUId)
        {
            var user = customerService.FindBy(x => x.UID == customerUId).FirstOrDefault();

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
            }

            var response = manageOrderService.GetOrderById(orderId);

            return Request.CreateResponse(HttpStatusCode.OK, new { response });
        }

        public class OrderCancel
        {
            public int OrderId { get; set; }

            public string CustomerId { get; set; }
        }

        public class OrderType
        {
            public int Id { get; set; }

            public string Name { get; set; }

        }

        private async Task<bool> sendAdminEmail(BankStatementAnalyzer.Models.Customer user, BankStatementAnalyzer.Models.Order order)
        {
            var emailTemplate = systemSettingService
                              .FindBy(
                               x => x.SettingType == SettingType.EmailTemplate)
                              .FirstOrDefault();

            var emailTemplateForNewOrder = systemSettingService
                                                  .FindBy(
                                                   x => x.SettingType == SettingType.EmailNewOrder)
                                                  .FirstOrDefault();

            var customerEmailTemplate = systemSettingService
                                                 .FindBy(
                                                  x => x.SettingType == SettingType.NewOrderCustomer)
                                                 .FirstOrDefault();

            var dbEmails = systemSettingService
                                      .FindBy(
                                       x => x.SettingType == SettingType.AdminEmails)
                                      .FirstOrDefault();

            var emails = dbEmails.Value.Split('|');

            string url = $"{Request.RequestUri.GetLeftPart(UriPartial.Authority)}/Orders/Details/{order.ID}";

            //switch (order.OrderType?.Name)
            //{
            //    case "One Touch":
            //        url = Url.Link("Default", new { Controller = "Orders", Action = "Details" });
            //        break;
            //    case "What's App":
            //        url = Url.Link("Default", new { Controller = "Orders", Action = "WhatsApp" });
            //        break;
            //    case "Customize":
            //        url = Url.Link("Default", new { Controller = "Orders", Action = "Customize" });
            //        break;
            //    case "Package":
            //        url = Url.Link("Default", new { Controller = "Orders", Action = "Package" });
            //        break;
            //    default:
            //        url = Url.Link("Default", new { Controller = "Orders", Action = "Index" });
            //        break;
            //}

            var adminName = ConfigurationManager.AppSettings["AdminName"];
            var fromMail = ConfigurationManager.AppSettings["fromMail"];

            var adminEmailBody = emailTemplate
                       .Value
                       .Replace
                        ("{{heading}}", $"New Order #{order.ID} - {order.OrderType.Name}")
                        .Replace("{{companyName}}", "E-Laundry")
                       .Replace
                        ("{{body}}", emailTemplateForNewOrder.Value
                        .Replace("{{name}}", adminName)
                       .Replace("{{CustomerName}}", user.Username)
                       .Replace("{{orderType}}", order.OrderType?.Name)
                       .Replace("{{orderId}}", order.ID.ToString())
                       .Replace("{{paymentype}}", order.PaymentType.ToString())
                       .Replace("{{url}}", url));

            List<string> lstAdmin = new List<string>();

            foreach (var email in emails)
            {
                lstAdmin.Add(email);
            }

            MailModel mailModelAdmin = new MailModel
            {
                FromId = fromMail,
                ToList = lstAdmin,
                Subject = $"New Order #{order.ID} - {order.OrderType.Name}",
                MessageBody = adminEmailBody
            };

            EmailHelper emailHelperAdmin = new EmailHelper(mailModelAdmin);
            try
            {
                emailHelperAdmin.Send();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                var customer = emailTemplate
                   .Value
                   .Replace
                    ("{{heading}}", $"New Order #{order.ID} - {order.OrderType.Name}")
                    .Replace("{{companyName}}", "E-Laundry")
                   .Replace
                    ("{{body}}", customerEmailTemplate.Value
                    .Replace("{{name}}", user.Username)
                   .Replace("{{orderid}}", order.ID.ToString())
                   .Replace("{{date}}", order.CreatedDate.ToString("dd MMM yyyy")));

                List<string> lstcst = new List<string>();
                lstcst.Add(user.Email);

                MailModel mailModelCustomer = new MailModel
                {
                    FromId = fromMail,
                    ToList = lstcst,
                    Subject = $"New Order #{order.ID} - {order.OrderType.Name}",
                    MessageBody = customer
                };

                EmailHelper emailHelperCustomer = new EmailHelper(mailModelCustomer);
                try
                {
                    emailHelperCustomer.Send();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message, ex);
                }
            }
            return true;
        }
    }
}