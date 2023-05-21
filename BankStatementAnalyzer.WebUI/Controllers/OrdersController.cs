using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.DataTables;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using BankStatementAnalyzer.WebUI.ViewModels;
using BankStatementAnalyzer.WebUI.ViewModels.Datatables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IDeliveryBoyService deliveryBoyService;
        private readonly IAddressService addressService;
        private readonly IPaymentService paymentService;
        private readonly IManageOrderService manageOrderService;
        private readonly IOrderDelvieryBoyMappingService orderDelvieryBoyMappingService;
        private readonly IOrderDetailService orderDetailService;
        private readonly ICustomerService customerService;
        private readonly ISystemSettingService systemSettingService;
        private readonly IOrderStatusMappingService orderStatusMappingService;
        private readonly IPushNotificationService pushNotificationService;
        private readonly IUserMasterService userMasterService;
        private readonly ILoggerFacade<BaseController> logger;
        private readonly ISendEmailService sendEmailService;
        private readonly IClothsService clothsService;
        private readonly IRateCardService rateCardService;
        private readonly IServiceTypeService serviceTypeService;
        private readonly IVendorService vendorService;
        private readonly IOrderVendorMappingService orderVendorMappingService;
        private readonly IPackageService packageService;

        private readonly int customize;
        private readonly int oneTouch;
        private readonly int whatsApp;
        private readonly int package;

        public OrdersController(IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger,
            IOrderService orderService,
            IDeliveryBoyService deliveryBoyService,
            IAddressService addressService,
            IPaymentService paymentService,
            IManageOrderService manageOrderService,
            IOrderDelvieryBoyMappingService orderDelvieryBoyMappingService,
            IOrderDetailService orderDetailService,
            ICustomerService customerService,
            ISystemSettingService systemSettingService,
            IOrderStatusMappingService orderStatusMappingService,
            IPushNotificationService pushNotificationService,
            IProductService productService,
            ISendEmailService sendEmailService,
            IClothsService clothsService,
            IServiceTypeService serviceTypeService,
            IRateCardService rateCardService,
            IVendorService vendorService,
            IOrderVendorMappingService orderVendorMappingService,
            IPackageService packageService) : base(userMasterService, logger)
        {
            this.orderService = orderService;
            this.deliveryBoyService = deliveryBoyService;
            this.addressService = addressService;
            this.paymentService = paymentService;
            this.manageOrderService = manageOrderService;
            this.orderDelvieryBoyMappingService = orderDelvieryBoyMappingService;
            this.orderDetailService = orderDetailService;
            this.customerService = customerService;
            this.systemSettingService = systemSettingService;
            this.orderStatusMappingService = orderStatusMappingService;
            this.pushNotificationService = pushNotificationService;
            this.userMasterService = userMasterService;
            this.logger = logger;
            this.sendEmailService = sendEmailService;
            this.clothsService = clothsService;
            this.serviceTypeService = serviceTypeService;
            this.rateCardService = rateCardService;
            this.vendorService = vendorService;
            this.orderVendorMappingService = orderVendorMappingService;
            this.packageService = packageService;

            customize = Convert.ToInt32(ConfigurationManager.AppSettings["customize"]);
            oneTouch = Convert.ToInt32(ConfigurationManager.AppSettings["oneTouch"]);
            whatsApp = Convert.ToInt32(ConfigurationManager.AppSettings["whatsApp"]);
            package = Convert.ToInt32(ConfigurationManager.AppSettings["package"]);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var linq = (from order in orderService.GetAll().ToList()
                        where order.OrderStatus != OrderStatus.Removed
                        select new Order
                        {
                            ID = order.ID,
                            OrderStatus = order.OrderStatus,
                            OrderType = order.OrderType,
                            CustomerName = order.Customer?.Username,
                            CustomerID = order.CustomerID,
                            CreatedDate = order.CreatedDate,
                            IsUrgent = order.IsUrgent
                        }).ToList();

            //var dbOrders = orderService.GetAll().Where(x => x.CompanyId == cid && x.OrderStatus != OrderStatus.Draft && x.OrderStatus != OrderStatus.Removed).OrderByDescending(x => x.OrderType == OrderType.Insta).ToList();
            return View(linq);
        }

        [HttpGet]
        public ActionResult OneTouch()
        {
            //order.OrderStatus == OrderStatus.Ordered && 
            var linq = (from order in orderService.GetAll().ToList()
                        where order.OrderTypeId == oneTouch
                        select new Order
                        {
                            ID = order.ID,
                            OrderStatus = order.OrderStatus,
                            OrderType = order.OrderType,
                            CustomerName = order.Customer?.Username,
                            CreatedDate = order.CreatedDate,
                            IsUrgent = order.IsUrgent
                        }).ToList();

            //var dbOrders = orderService.GetAll().Where(x => x.CompanyId == cid && x.OrderStatus != OrderStatus.Draft && x.OrderStatus != OrderStatus.Removed).OrderByDescending(x => x.OrderType == OrderType.Insta).ToList();
            return View(linq);
        }

        [HttpGet]
        public ActionResult WhatsApp()
        {
            //order.OrderStatus == OrderStatus.Ordered &&
            var linq = (from order in orderService.GetAll().ToList()
                        where order.OrderTypeId == whatsApp
                        select new Order
                        {
                            ID = order.ID,
                            OrderStatus = order.OrderStatus,
                            OrderType = order.OrderType,
                            CustomerName = order.Customer?.Username,
                            CustomerID = order.CustomerID,
                            CreatedDate = order.CreatedDate,
                            IsUrgent = order.IsUrgent
                        }).ToList();

            //var dbOrders = orderService.GetAll().Where(x => x.CompanyId == cid && x.OrderStatus != OrderStatus.Draft && x.OrderStatus != OrderStatus.Removed).OrderByDescending(x => x.OrderType == OrderType.Insta).ToList();
            return View(linq);
        }

        [HttpGet]
        public ActionResult Customize()
        {
            //order.OrderStatus == OrderStatus.Ordered &&
            var linq = (from order in orderService.GetAll().ToList()
                        where order.OrderTypeId == customize
                        select new Order
                        {
                            ID = order.ID,
                            OrderStatus = order.OrderStatus,
                            OrderType = order.OrderType,
                            CustomerName = order.Customer?.Username,
                            CreatedDate = order.CreatedDate,
                            IsUrgent = order.IsUrgent
                        }).ToList();

            //var dbOrders = orderService.GetAll().Where(x => x.CompanyId == cid && x.OrderStatus != OrderStatus.Draft && x.OrderStatus != OrderStatus.Removed).OrderByDescending(x => x.OrderType == OrderType.Insta).ToList();
            return View(linq);
        }

        [HttpGet]
        public ActionResult Package()
        {
            //order.OrderStatus == OrderStatus.Ordered && 
            var linq = (from order in orderService.GetAll().ToList()
                        join dbpackage in packageService.GetAll().ToList() on order.PackageId equals dbpackage.ID
                        where order.OrderTypeId == package
                        select new Order
                        {
                            ID = order.ID,
                            OrderStatus = order.OrderStatus,
                            OrderType = order.OrderType,
                            CustomerName = order.Customer?.Username,
                            CreatedDate = order.CreatedDate,
                            IsUrgent = order.IsUrgent,
                            Package = dbpackage
                        }).ToList();

            //var dbOrders = orderService.GetAll().Where(x => x.CompanyId == cid && x.OrderStatus != OrderStatus.Draft && x.OrderStatus != OrderStatus.Removed).OrderByDescending(x => x.OrderType == OrderType.Insta).ToList();
            return View(linq);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var order = manageOrderService.GetOrderDetailsByOrderId(id);
            return View(order);
        }

        [HttpGet]
        public ActionResult Assign(int id)
        {
            Load();
            var order = manageOrderService.GetOrderDetailsByOrderId(id);
            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> Assign(int id, BusinessLogic.ViewModels.OrderVM orderVM)
        {
            if (orderVM.DeliveryBoyId == 0)
            {
                Load();
                var order = manageOrderService.GetOrderDetailsByOrderId(id);

                ModelState.AddModelError("DeliveryBoyId", "Delivery boy required.");
                //ModelState.AddModelError("Remark", "Remark required.");

                TempData["warning-message"] = "Delivery boy required.";

                return View(order);
            }

            var model = orderService.FindBy(x => x.ID == id).FirstOrDefault();

            if (model.AddressID == null || model.AddressID == 0)
            {
                Load();
                var order = manageOrderService.GetOrderDetailsByOrderId(id);

                //ModelState.AddModelError("Remark", "Remark required.");

                TempData["warning-message"] = "Please select Address for Pick-Up.";

                return View(order);
            }

            model.OrderStatus = OrderStatus.AssignedForPickUp;
            model.UpdatedBy = UserName;
            model.UpdatedDate = DateTime.Now.Date;
            orderService.Edit(model);
            orderService.Save();

            var dbOrderStatus = orderStatusMappingService.FindBy(x => x.OrderId == model.ID && x.OrderStatus == OrderStatus.AssignedForPickUp).FirstOrDefault();

            if (dbOrderStatus != null)
            {
                dbOrderStatus.EstimatedDate = DateTime.Now;
                orderStatusMappingService.Edit(dbOrderStatus);
                orderStatusMappingService.Save();
            }

            var orderDetails = orderDetailService.FindBy(x => x.OrderID == model.ID && x.OrderStatus == OrderStatus.Ordered).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = OrderStatus.AssignedForPickUp;
                orderDetailService.Edit(orderDetail);
                orderDetailService.Save();
            }

            OrderDelvieryBoyMapping orderDelvieryBoyMapping = new OrderDelvieryBoyMapping
            {
                OrderId = id,
                DeliveryBoyId = orderVM.DeliveryBoyId,
                MappedFor = MappedFor.CustomerPickUp,
                CreatedBy = UserName,
                CreatedDate = DateTime.Now
            };

            orderDelvieryBoyMappingService.Add(orderDelvieryBoyMapping);
            orderDelvieryBoyMappingService.Save();

            #region Trigger notification
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == orderVM.DeliveryBoyId).FirstOrDefault();
            var customer = model.Customer?.Username;
            var deviceId = model.Customer?.DeviceId;
            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();
            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();
            if (serverKey != null && pushBaseUrl != null && deliveryBoy != null && !string.IsNullOrWhiteSpace(deviceId))
            {
                var message = MessageBuilder(deliveryBoy, customer, model.ID);
                var result = await pushNotificationService.Send("Order pickup initiated", message, deviceId, serverKey.Value, pushBaseUrl.Value);
            }

            string url = $"{HttpContext.Request.Url.Scheme}://{HttpContext.Request.Url.Host}/Orders/Details/{model.ID}";
            var emailResult = await sendEmailService.SendAdminEmail(model.Customer, model, deliveryBoy, url, $"New order #{id}");
            if (emailResult.Item1)
            {
                logger.Error(emailResult.Item2.Message, emailResult.Item2);
            }
            #endregion

            TempData["success-message"] = "Order assigned to delivery boy successfully.";

            switch (model.OrderType?.Name)
            {
                case "One Touch":
                    return RedirectToAction("OneTouch");
                case "What's App":
                    return RedirectToAction("WhatsApp");
                case "Customize":
                    return RedirectToAction("Customize");
                case "Package":
                    return RedirectToAction("Package");
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Cancel(int id)
        {
            Load();
            var order = manageOrderService.GetOrderDetailsByOrderId(id);
            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(int id, BusinessLogic.ViewModels.OrderVM orderVM)
        {
            var model = orderService.FindBy(x => x.ID == id).FirstOrDefault();

            model.OrderStatus = OrderStatus.Cancelled;
            model.Remark = orderVM.Remark;
            model.UpdatedBy = UserName;
            model.UpdatedDate = DateTime.Now.Date;
            orderService.Edit(model);
            orderService.Save();

            OrderStatusMapping orderStatusMapping = new OrderStatusMapping
            {
                OrderStatus = OrderStatus.Cancelled,
                CreatedOn = DateTime.Now,
                EstimatedDate = DateTime.Now,
                OrderId = model.ID
            };

            orderStatusMappingService.Add(orderStatusMapping);
            orderStatusMappingService.Save();

            var orderDetails = orderDetailService.FindBy(x => x.OrderID == model.ID && x.OrderStatus == OrderStatus.Ordered).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = OrderStatus.Cancelled;
                orderDetailService.Edit(orderDetail);
                orderDetailService.Save();
            }

            #region Trigger notification
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == orderVM.DeliveryBoyId).FirstOrDefault();
            var customer = model.Customer?.Username;
            var deviceId = model.Customer?.DeviceId;
            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();
            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();
            if (serverKey != null && pushBaseUrl != null && deliveryBoy != null && !string.IsNullOrWhiteSpace(deviceId))
            {
                var message = MessageBuilderForCancel(deliveryBoy, customer, model.ID);
                var result = await pushNotificationService.Send("Order cancelled", message, deviceId, serverKey.Value, pushBaseUrl.Value);
            }
            #endregion

            TempData["success-message"] = "Order cancelled successfully.";

            switch (model.OrderType?.Name)
            {
                case "One Touch":
                    return RedirectToAction("OneTouch");
                case "What's App":
                    return RedirectToAction("WhatsApp");
                case "Customize":
                    return RedirectToAction("Customize");
                case "Package":
                    return RedirectToAction("Package");
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult OrderListItem(int id, string redirectAction, int orderItemId = 0, bool isOrderItemEdit = false)
        {
            if (id == 0)
            {
                TempData["warning-message"] = "Invalid order details.";
                return RedirectToAction(redirectAction);
            }

            var order = orderService.FindBy(x => x.ID == id).FirstOrDefault();

            OrderVM orderVM = new OrderVM
            {
                Order = order,
                RedirectAction = redirectAction
            };

            if (isOrderItemEdit)
            {
                var orderDetail = orderDetailService.FindBy(x => x.Id == orderItemId).FirstOrDefault();
                orderVM.OrderDetail = orderDetail;
                orderVM.IsEdit = true;
            }

            Load();

            return View(orderVM);
        }

        [HttpPost]
        public ActionResult OrderListItem(int id, string redirectAction, OrderVM orderVM, int orderItemId = 0, bool isOrderItemEdit = false, bool isOrderItemDelete = false)
        {
            Load();
            if (redirectAction != "Package" && orderVM.OrderDetail.Price == 0)
            {
                TempData["warning-message"] = "Price can not be 0";
                return View(orderVM);
            }

            if (orderVM.OrderDetail.Quantity == 0)
            {
                TempData["warning-message"] = "Quantity can not be 0.";
                return View(orderVM);
            }

            if (isOrderItemDelete)
            {
                var dbOrderDetail = orderDetailService.FindBy(x => x.Id == orderItemId).FirstOrDefault();

                orderDetailService.Delete(dbOrderDetail);
                orderDetailService.Save();

                TempData["success-message"] = "Order item deleted successfully.";
                return RedirectToAction(redirectAction);
            }

            var order = orderService.FindBy(x => x.ID == id).FirstOrDefault();
            order.IsUrgent = orderVM.OrderDetail.IsUrgent;
            orderService.Edit(order);
            orderService.Save();

            if (isOrderItemEdit)
            {
                var dbOrderDetail = orderDetailService.FindBy(x => x.Id == orderItemId).FirstOrDefault();

                if (redirectAction == "Package")
                {
                    var dbOrder = orderService.FindBy(x => x.ID == id).FirstOrDefault();

                    dbOrderDetail.Price = 0;
                    dbOrderDetail.TotalPayable = dbOrder.Package == null ? 0 : Convert.ToDecimal(dbOrder.Package.Rate);
                }
                else
                {
                    dbOrderDetail.Price = orderVM.OrderDetail.Price;
                    dbOrderDetail.TotalPayable = orderVM.OrderDetail.Price * orderVM.OrderDetail.Quantity;
                }

                dbOrderDetail.Price = orderVM.OrderDetail.Price;
                dbOrderDetail.IsUrgent = orderVM.OrderDetail.IsUrgent;
                dbOrderDetail.Quantity = orderVM.OrderDetail.Quantity;
                dbOrderDetail.ClothsID = orderVM.OrderDetail.ClothsID;
                dbOrderDetail.ServiceTypeID = orderVM.OrderDetail.ServiceTypeID;
                dbOrderDetail.Description = orderVM.OrderDetail.Description;
                dbOrderDetail.OrderStatus = OrderStatus.AssignedForPickUp;
                dbOrderDetail.Remark = orderVM.OrderDetail.Remark;
                orderDetailService.Edit(dbOrderDetail);
                orderDetailService.Save();

                TempData["success-message"] = "Order item edited successfully.";
                return RedirectToAction(redirectAction);
            }
            else
            {
                orderVM.OrderDetail.TotalPayable = orderVM.OrderDetail.Price * orderVM.OrderDetail.Quantity;
                orderVM.OrderDetail.OrderID = id;
                orderVM.OrderDetail.OrderStatus = OrderStatus.AssignedForPickUp;
                if (redirectAction == "Package")
                {
                    var dbOrder = orderService.FindBy(x => x.ID == id).FirstOrDefault();

                    orderVM.OrderDetail.Price = 0;
                    orderVM.OrderDetail.TotalPayable = dbOrder.Package == null ? 0 : Convert.ToDecimal(dbOrder.Package.Rate);
                }
                orderDetailService.Add(orderVM.OrderDetail);
                orderDetailService.Save();

                TempData["success-message"] = "Order item created successfully.";
            }

            if (orderVM.CreateAnother)
            {
                return RedirectToAction("OrderListItem", new { id = id, redirectAction = redirectAction });
            }

            return RedirectToAction(redirectAction);
        }

        [HttpPost]
        public async Task<ActionResult> MarkAsPickedUp(int id, string redirectAction, OrderVM orderVM)
        {
            var model = orderService.FindBy(x => x.ID == id).FirstOrDefault();

            if (model.OrderDetails.Count() == 0)
            {
                TempData["warning-message"] = "Please add atleast 1 order item to proceed further.";

                return RedirectToAction("OrderListItem", new { id, redirectAction });
            }

            model.OrderStatus = OrderStatus.PickedUp;
            model.UpdatedBy = UserName;
            model.UpdatedDate = DateTime.Now.Date;
            orderService.Edit(model);
            orderService.Save();

            var dbOrderStatus = orderStatusMappingService.FindBy(x => x.OrderId == model.ID && x.OrderStatus == OrderStatus.PickedUp).FirstOrDefault();

            if (dbOrderStatus != null)
            {
                dbOrderStatus.EstimatedDate = DateTime.Now;
                orderStatusMappingService.Edit(dbOrderStatus);
                orderStatusMappingService.Save();
            }

            var orderDetails = orderDetailService.FindBy(x => x.OrderID == model.ID && x.OrderStatus == OrderStatus.AssignedForPickUp).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = OrderStatus.PickedUp;
                orderDetailService.Edit(orderDetail);
                orderDetailService.Save();
            }

            #region Trigger notification
            var orderDeliveryBoy = orderDelvieryBoyMappingService.FindBy(x => x.OrderId == model.ID).FirstOrDefault();
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == orderDeliveryBoy.DeliveryBoyId).FirstOrDefault();
            var customer = model.Customer?.Username;
            var deviceId = model.Customer?.DeviceId;
            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();
            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();
            if (serverKey != null && pushBaseUrl != null && deliveryBoy != null && !string.IsNullOrWhiteSpace(deviceId))
            {
                var message = MessageBuilderForPickUp(deliveryBoy, customer, model.ID);
                var result = await pushNotificationService.Send("Order picked-up successfully", message, deviceId, serverKey.Value, pushBaseUrl.Value);
            }
            #endregion

            return RedirectToAction(redirectAction);
        }

        [HttpGet]
        public ActionResult AssignToVendor(int id, string redirectAction)
        {
            LoadVendor();
            var order = manageOrderService.GetOrderDetailsByOrderId(id);
            order.RedirectAction = redirectAction;
            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> AssignToVendor(int id, string redirectAction, BusinessLogic.ViewModels.OrderVM orderVM)
        {
            if (orderVM.VendorId == 0)
            {
                LoadVendor();
                var order = manageOrderService.GetOrderDetailsByOrderId(id);

                ModelState.AddModelError("VendorId", "Vendor required.");

                TempData["warning-message"] = "Please select Vendor to proceed.";

                return View(order);
            }

            var model = orderService.FindBy(x => x.ID == id).FirstOrDefault();

            model.OrderStatus = OrderStatus.UnderProcessing;
            model.UpdatedBy = UserName;
            model.UpdatedDate = DateTime.Now.Date;
            orderService.Edit(model);
            orderService.Save();

            var dbOrderStatus = orderStatusMappingService.FindBy(x => x.OrderId == model.ID && x.OrderStatus == OrderStatus.UnderProcessing).FirstOrDefault();

            if (dbOrderStatus != null)
            {
                dbOrderStatus.EstimatedDate = DateTime.Now;
                orderStatusMappingService.Edit(dbOrderStatus);
                orderStatusMappingService.Save();
            }

            var orderDetails = orderDetailService.FindBy(x => x.OrderID == model.ID && x.OrderStatus == OrderStatus.PickedUp).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = OrderStatus.UnderProcessing;
                orderDetailService.Edit(orderDetail);
                orderDetailService.Save();
            }

            OrderVendorMapping orderVendorMapping = new OrderVendorMapping
            {
                CreatedBy = UserName,
                OrderId = model.ID,
                VendorId = orderVM.VendorId
            };

            orderVendorMappingService.Add(orderVendorMapping);
            orderVendorMappingService.Save();

            #region Trigger notification
            var customer = model.Customer?.Username;
            var deviceId = model.Customer?.DeviceId;
            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();
            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();
            if (serverKey != null && pushBaseUrl != null && !string.IsNullOrWhiteSpace(deviceId))
            {
                var message = MessageBuilderForUnderProcessing(customer, model.ID);
                var result = await pushNotificationService.Send("Order under process", message, deviceId, serverKey.Value, pushBaseUrl.Value);
            }
            #endregion

            TempData["success-message"] = "Order assigned to vendor successfully.";
            return RedirectToAction(redirectAction);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult PickupFromVendor(int id, string redirectAction)
        {
            Load();
            var order = manageOrderService.GetOrderDetailsByOrderId(id);
            order.RedirectAction = redirectAction;
            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> PickupFromVendor(int id, string redirectAction, BusinessLogic.ViewModels.OrderVM orderVM)
        {
            if (orderVM.DeliveryBoyId == 0)
            {
                Load();
                var order = manageOrderService.GetOrderDetailsByOrderId(id);

                ModelState.AddModelError("DeliveryBoyId", "Delivery boy required.");
                TempData["warning-message"] = "Delivery boy required.";

                return View(order);
            }

            var model = orderService.FindBy(x => x.ID == id).FirstOrDefault();
            model.OrderStatus = OrderStatus.ReadyForDelivery;
            model.UpdatedBy = UserName;
            model.UpdatedDate = DateTime.Now.Date;
            orderService.Edit(model);
            orderService.Save();

            var dbOrderStatus = orderStatusMappingService.FindBy(x => x.OrderId == model.ID && x.OrderStatus == OrderStatus.ReadyForDelivery).FirstOrDefault();

            if (dbOrderStatus != null)
            {
                dbOrderStatus.EstimatedDate = DateTime.Now;
                orderStatusMappingService.Edit(dbOrderStatus);
                orderStatusMappingService.Save();
            }

            var orderDetails = orderDetailService.FindBy(x => x.OrderID == model.ID && x.OrderStatus == OrderStatus.UnderProcessing).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = OrderStatus.ReadyForDelivery;
                orderDetailService.Edit(orderDetail);
                orderDetailService.Save();
            }

            OrderDelvieryBoyMapping orderDelvieryBoyMapping = new OrderDelvieryBoyMapping
            {
                OrderId = id,
                DeliveryBoyId = orderVM.DeliveryBoyId,
                MappedFor = MappedFor.VendorPickUp,
                CreatedBy = UserName,
                CreatedDate = DateTime.Now
            };

            orderDelvieryBoyMappingService.Add(orderDelvieryBoyMapping);
            orderDelvieryBoyMappingService.Save();

            #region Trigger notification
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == orderVM.DeliveryBoyId).FirstOrDefault();
            var vendorId = orderVendorMappingService.FindBy(x => x.OrderId == model.ID).FirstOrDefault().VendorId;
            var vendor = vendorService.FindBy(x => x.ID == vendorId).FirstOrDefault();
            var customer = model.Customer?.Username;
            var deviceId = model.Customer?.DeviceId;
            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();
            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();
            if (serverKey != null && pushBaseUrl != null && deliveryBoy != null && !string.IsNullOrWhiteSpace(deviceId))
            {
                var message = MessageBuilderForOrderReady(customer, model.ID);
                var result = await pushNotificationService.Send("Order ready and packed", message, deviceId, serverKey.Value, pushBaseUrl.Value);
            }

            string url = $"{HttpContext.Request.Url.Scheme}://{HttpContext.Request.Url.Host}/Orders/Details/{model.ID}";
            var emailResult = await sendEmailService.SendAdminEmail(model.Customer, model, deliveryBoy, url, $"Pickup From {vendor.Name} - #{id}", $"{vendor?.Latitude},{vendor?.Longitude}");
            if (emailResult.Item1)
            {
                logger.Error(emailResult.Item2.Message, emailResult.Item2);
            }
            #endregion

            TempData["success-message"] = "Order assigned to delivery boy for pickup from vendor successfully.";
            return RedirectToAction(redirectAction);
        }

        [HttpGet]
        public ActionResult AssignForDelviery(int id, string redirectAction)
        {
            Load();
            var order = manageOrderService.GetOrderDetailsByOrderId(id);
            order.RedirectAction = redirectAction;
            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> AssignForDelviery(int id, string redirectAction, BusinessLogic.ViewModels.OrderVM orderVM)
        {
            if (orderVM.DeliveryBoyId == 0)
            {
                Load();
                var order = manageOrderService.GetOrderDetailsByOrderId(id);

                ModelState.AddModelError("DeliveryBoyId", "Delivery boy required.");
                //ModelState.AddModelError("Remark", "Remark required.");

                TempData["warning-message"] = "Delivery boy required.";

                return View(order);
            }

            var model = orderService.FindBy(x => x.ID == id).FirstOrDefault();

            if (model.AddressID == null || model.AddressID == 0)
            {
                Load();
                var order = manageOrderService.GetOrderDetailsByOrderId(id);

                //ModelState.AddModelError("Remark", "Remark required.");

                TempData["warning-message"] = "Please select Address for Pick-Up.";

                return View(order);
            }

            model.OrderStatus = OrderStatus.AssignedForDelivery;
            model.UpdatedBy = UserName;
            model.UpdatedDate = DateTime.Now.Date;
            orderService.Edit(model);
            orderService.Save();

            var dbOrderStatus = orderStatusMappingService.FindBy(x => x.OrderId == model.ID && x.OrderStatus == OrderStatus.AssignedForDelivery).FirstOrDefault();

            if (dbOrderStatus != null)
            {
                dbOrderStatus.EstimatedDate = DateTime.Now;
                orderStatusMappingService.Edit(dbOrderStatus);
                orderStatusMappingService.Save();
            }

            var orderDetails = orderDetailService.FindBy(x => x.OrderID == model.ID && x.OrderStatus == OrderStatus.ReadyForDelivery).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = OrderStatus.AssignedForDelivery;
                orderDetailService.Edit(orderDetail);
                orderDetailService.Save();
            }

            OrderDelvieryBoyMapping orderDelvieryBoyMapping = new OrderDelvieryBoyMapping
            {
                OrderId = id,
                DeliveryBoyId = orderVM.DeliveryBoyId,
                MappedFor = MappedFor.CustomerDelivery,
                CreatedBy = UserName,
                CreatedDate = DateTime.Now
            };

            orderDelvieryBoyMappingService.Add(orderDelvieryBoyMapping);
            orderDelvieryBoyMappingService.Save();

            #region Trigger notification
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == orderVM.DeliveryBoyId).FirstOrDefault();
            var customer = model.Customer?.Username;
            var deviceId = model.Customer?.DeviceId;
            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();
            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();
            if (serverKey != null && pushBaseUrl != null && deliveryBoy != null && !string.IsNullOrWhiteSpace(deviceId))
            {
                var message = MessageBuilderForDelivery(deliveryBoy, customer, model.ID);
                var result = await pushNotificationService.Send("Order out for delivery", message, deviceId, serverKey.Value, pushBaseUrl.Value);
            }

            string url = $"{HttpContext.Request.Url.Scheme}://{HttpContext.Request.Url.Host}/Orders/Details/{model.ID}";
            var emailResult = await sendEmailService.SendAdminEmail(model.Customer, model, deliveryBoy, url, $"Order delivery #{model.ID}");
            if (emailResult.Item1)
            {
                logger.Error(emailResult.Item2.Message, emailResult.Item2);
            }
            #endregion

            TempData["success-message"] = "Order assigned to delivery boy successfully.";
            return RedirectToAction(redirectAction);
        }

        [HttpGet]
        public ActionResult MarkDelivered(int id, string redirectAction)
        {
            var order = manageOrderService.GetOrderDetailsByOrderId(id);
            order.RedirectAction = redirectAction;
            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> MarkDelivered(int id, string redirectAction, BusinessLogic.ViewModels.OrderVM orderVM)
        {
            decimal subtractedAmount = orderVM.PaidAmount - orderVM.Order.PaidAmount;

            if (subtractedAmount > 0)//positive
            {
                orderVM.Order.PaymentStatus = "BALANCE";
            }
            else if(subtractedAmount < 0)//negative
            {
                orderVM.Order.PaymentStatus = "ADVANCE";
            }
            else// equal zero
            {
                orderVM.Order.PaymentStatus = "FULL";
            }


            if (orderVM.Order.PaidAmount <= 0 && orderVM.Order.PaymentType == PaymentType.COD)
            {
                TempData["warning-message"] = "Amount cannot be 0";
                return RedirectToAction("MarkDelivered", new { id, redirectAction });
            }

            var model = orderService.FindBy(x => x.ID == id).FirstOrDefault();

            if (orderVM.PaidAmount > orderVM.Order.PaidAmount || orderVM.PaidAmount < orderVM.Order.PaidAmount)
            {
                TempData["warning-message"] = "Please enter total paid amount.";

                return RedirectToAction("MarkDelivered", new { id, redirectAction });
            }

            model.Tip = orderVM.Order.Tip;
            model.MerchantCode = orderVM.Order.MerchantCode;
            model.Total = orderVM.Order.PaidAmount;
            model.PaidAmount = orderVM.Order.PaidAmount;
            model.PaymentType = orderVM.Order.PaymentType;
            model.OrderStatus = OrderStatus.Delivered;
            model.UpdatedBy = UserName;
            model.UpdatedDate = DateTime.Now.Date;
            model.DeliveryDate = DateTime.Now.Date;
            orderService.Edit(model);
            orderService.Save();

            var dbOrderStatus = orderStatusMappingService.FindBy(x => x.OrderId == model.ID && x.OrderStatus == OrderStatus.Delivered).FirstOrDefault();

            if (dbOrderStatus != null)
            {
                dbOrderStatus.EstimatedDate = DateTime.Now;
                orderStatusMappingService.Edit(dbOrderStatus);
                orderStatusMappingService.Save();
            }

            var orderDetails = orderDetailService.FindBy(x => x.OrderID == model.ID && x.OrderStatus == OrderStatus.AssignedForDelivery).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = OrderStatus.Delivered;
                orderDetailService.Edit(orderDetail);
                orderDetailService.Save();
            }

            #region Trigger notification
            var orderDeliveryBoy = orderDelvieryBoyMappingService.FindBy(x => x.OrderId == model.ID).FirstOrDefault();
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == orderDeliveryBoy.DeliveryBoyId).FirstOrDefault();
            var customer = model.Customer?.Username;
            var deviceId = model.Customer?.DeviceId;
            var serverKey = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotificationServerKey).FirstOrDefault();
            var pushBaseUrl = systemSettingService.FindBy(x => x.SettingType == SettingType.PushNotifcationURL).FirstOrDefault();
            if (serverKey != null && pushBaseUrl != null && deliveryBoy != null && !string.IsNullOrWhiteSpace(deviceId))
            {
                var message = MessageBuilderForDelivered(customer, model.ID);
                var result = await pushNotificationService.Send("Order delivered successfully", message, deviceId, serverKey.Value, pushBaseUrl.Value);

                await sendAdminEmail(model.Customer, model);
            }
            #endregion

            return RedirectToAction(redirectAction);
        }

        private async Task<bool> sendAdminEmail(BankStatementAnalyzer.Models.Customer user, BankStatementAnalyzer.Models.Order order)
        {
            var emailTemplate = systemSettingService
                              .FindBy(
                               x => x.SettingType == SettingType.EmailTemplate)
                              .FirstOrDefault();

            var customerEmailTemplate = systemSettingService
                                                 .FindBy(
                                                  x => x.SettingType == SettingType.OrderCompletedCustomer)
                                                 .FirstOrDefault();


            var fromMail = ConfigurationManager.AppSettings["fromMail"];

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                var customer = emailTemplate
                   .Value
                   .Replace
                    ("{{heading}}", "Order completed successfully")
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
                    Subject = "Order completed",
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

        public async Task<ActionResult> SendEmail(int id)
        {
            var deliveryBoyId = orderDelvieryBoyMappingService.FindBy(x => x.OrderId == id).FirstOrDefault();
            if (deliveryBoyId == null)
            {
                TempData["warning-message"] = "Selected order is not mapped to any delivery boy.";

                return RedirectToAction("Index");
            }

            var model = orderService.FindBy(x => x.ID == id).FirstOrDefault();
            var deliveryBoy = deliveryBoyService.FindBy(x => x.ID == deliveryBoyId.DeliveryBoyId).FirstOrDefault();

            if (deliveryBoy == null)
            {
                TempData["warning-message"] = "Delivery boy details not found.";

                return RedirectToAction("Index");
            }

            var add = "";
            var sub = $"pickup order #{model.ID}";
            if (deliveryBoyId.MappedFor == MappedFor.VendorPickUp)
            {
                var vendor = orderVendorMappingService.FindBy(x => x.OrderId == id).FirstOrDefault();
                var dbVendor = vendorService.FindBy(x => x.ID == vendor.ID).FirstOrDefault();

                add = dbVendor?.Address;
                sub = $"pickup order from vendor #{model.ID}";
            }

            string url = $"{HttpContext.Request.Url.Scheme}://{HttpContext.Request.Url.Host}/Orders/Details/{model.ID}";
            var emailResult = await sendEmailService.SendAdminEmail(model.Customer, model, deliveryBoy, url, sub, string.IsNullOrWhiteSpace(add) ? null : add);
            if (!emailResult.Item1)
            {
                logger.Error(emailResult.Item2.Message, emailResult.Item2);
            }

            TempData["success-message"] = $"Email sent to {deliveryBoy.Email} successfully.";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public JsonResult GetPrice(int clothId, int serviceTypeId)
        {
            var rateCard = rateCardService.FindBy(x => x.ClothId == clothId && x.ServiceTypeId == serviceTypeId).FirstOrDefault();
            if (rateCard == null)
            {
                return Json(new { result = false, normalPrice = 0, urgentRate = 0 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = true, normalPrice = rateCard.NormalRate, urgentRate = rateCard.UrgetRate }, JsonRequestBehavior.AllowGet);
        }

        private void Load()
        {
            ViewBag.DeliveryBoy = deliveryBoyService
                                 .GetAll()
                                 .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name });

            ViewBag.ServiceType = serviceTypeService
                                 .GetAll()
                                 .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name });

            var cloths = clothsService
                                .GetAll().ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var cloth in cloths)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Value = cloth.ID.ToString(),
                    Text = $"{cloth.Category.Name} - {cloth.Name}"
                };

                selectListItems.Add(selectListItem);
            }

            ViewBag.Cloths = selectListItems;
        }

        private void LoadVendor()
        {
            ViewBag.Vendor = vendorService.GetAll()
                            .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name })
                            .ToList();
        }

        private string MessageBuilder(DeliveryBoy deliveryBoy, string customer, int iD)
        {
            return $"Dear {customer}, your order-{iD} has been assigned for Pick-Up to {deliveryBoy.Name} on {DateTime.Now.ToString("dd MM yyyy hh:mm:ss")} you can reach delivery boy on {deliveryBoy.Number} , Thanks for choosing E-Laundry.";
        }

        private string MessageBuilderForCancel(DeliveryBoy deliveryBoy, string customer, int iD)
        {
            return $"Dear {customer}, your order-{iD} has been cancelled by admin on {DateTime.Now.ToString("dd MM yyyy hh:mm:ss")} , Thanks for choosing E-Laundry.";
        }

        private string MessageBuilderForPickUp(DeliveryBoy deliveryBoy, string customer, int iD)
        {
            return $"Dear {customer}, your order-{iD} has been picked by {deliveryBoy.Name} on {DateTime.Now.ToString("dd MM yyyy hh:mm:ss")} you can reach delivery boy on {deliveryBoy.Number} , Thanks for choosing E-Laundry.";
        }

        private string MessageBuilderForUnderProcessing(string customer, int iD)
        {
            return $"Dear {customer}, your order-{iD} is under process on {DateTime.Now.ToString("dd MM yyyy hh:mm:ss")} we will keep updated about order status, Thanks for choosing E-Laundry.";
        }

        private string MessageBuilderForOrderReady(string customer, int iD)
        {
            return $"Dear {customer}, your order-{iD} is ready and packed on {DateTime.Now.ToString("dd MM yyyy hh:mm:ss")} we keep updated about order status, Thanks for choosing E-Laundry.";
        }

        private string MessageBuilderForDelivery(DeliveryBoy deliveryBoy, string customer, int iD)
        {
            return $"Dear {customer}, your order-{iD} is ready and out for delivery on {DateTime.Now.ToString("dd MM yyyy hh:mm:ss")} you can reach our delivery executive on  {deliveryBoy.Number}, Thanks for choosing E-Laundry.";
        }

        private string MessageBuilderForDelivered(string customer, int iD)
        {
            return $"Dear {customer}, your order-{iD} delivered successfully on {DateTime.Now.ToString("dd MM yyyy hh:mm:ss")} , Thanks for choosing E-Laundry.";
        }
    }
}