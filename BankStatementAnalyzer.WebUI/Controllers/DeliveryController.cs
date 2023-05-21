using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class DeliveryController : BaseController
    {
        private readonly IDeliveryBoyService deliveryBoyService;
        private readonly IReturnRequestDeliveryBoyMappingService returnRequestDeliveryBoyMappingService;
        private readonly IReturnRequestService returnRequestService;
        private readonly IManageOrderService manageOrderService;
        private readonly IOrderDelvieryBoyMappingService orderDelvieryBoyMappingService;
        private readonly IOrderDetailService orderDetailService;
        private readonly IOrderService orderService;
        private readonly ICustomerRefferalCodeMappingService customerRefferalCodeMappingService;
        private readonly IOrderStatusMappingService orderStatusMappingService;
        private readonly ICustomerCreditMappingService customerCreditMappingService;
        private readonly ICustomerCreditHistoryService customerCreditHistoryService;
        private readonly ISystemSettingService systemSettingService;
        private readonly IWalletService walletService;
        private readonly ICustomerService customerService;

        public DeliveryController(IDeliveryBoyService deliveryBoyService,
            IUserMasterService userMasterService,
            ILoggerFacade<BaseController> logger,
            IReturnRequestService returnRequestService,
            IReturnRequestDeliveryBoyMappingService returnRequestDeliveryBoyMappingService,
            IManageOrderService manageOrderService,
            IOrderDelvieryBoyMappingService orderDelvieryBoyMappingService,
            IOrderDetailService orderDetailService,
            IOrderService orderService,
            ICustomerRefferalCodeMappingService customerRefferalCodeMappingService,
            IOrderStatusMappingService orderStatusMappingService,
            ICustomerCreditMappingService customerCreditMappingService,
            ICustomerCreditHistoryService customerCreditHistoryService,
            ISystemSettingService systemSettingService,
            IWalletService walletService,
            ICustomerService customerService) : base(userMasterService, logger)
        {
            this.deliveryBoyService = deliveryBoyService;
            this.returnRequestService = returnRequestService;
            this.returnRequestDeliveryBoyMappingService = returnRequestDeliveryBoyMappingService;
            this.manageOrderService = manageOrderService;
            this.orderDelvieryBoyMappingService = orderDelvieryBoyMappingService;
            this.orderDetailService = orderDetailService;
            this.orderService = orderService;
            this.customerRefferalCodeMappingService = customerRefferalCodeMappingService;
            this.orderStatusMappingService = orderStatusMappingService;
            this.customerCreditMappingService = customerCreditMappingService;
            this.customerCreditHistoryService = customerCreditHistoryService;
            this.systemSettingService = systemSettingService;
            this.walletService = walletService;
            this.customerService = customerService;
        }

        public ActionResult Index()
        {
            var user = GetLoggedInUser();

            List<Order> orders = new List<Order>();

            if (user.Roles.Any(x => x.Name == "Delivery Boy"))
            {
                var assignedOrders = orderDelvieryBoyMappingService.FindBy(x => x.DeliveryBoyId == user.ID).ToList();
                foreach (var assignedOrder in assignedOrders)
                {
                    var order = orderService.FindBy(x => x.OrderStatus == OrderStatus.AssignedForPickUp && x.ID == assignedOrder.OrderId).FirstOrDefault();
                    if (order != null)
                    {
                        getCustomerName(order, null);

                        orders.Add(order);
                    }
                }
            }
            return View(orders);
        }

        private void Load()
        {
            ViewBag.DeliveryBoy = deliveryBoyService
                                 .GetAll()
                                 .Where(x => x.Status == true)
                                 .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name });
        }

        private void Populate()
        {
            List<SelectListItem> selectLists = new List<SelectListItem>
            {
                new SelectListItem {Value = "1" , Text ="COD" },
                new SelectListItem {Value = "2" , Text ="CreditAmount" },
                new SelectListItem {Value = "3" , Text ="AdvanceAmount" }
            };

            ViewBag.SelectList = selectLists;
        }

        private void getCustomerName(Order order, ReturnRequest returnRequest)
        {
            if (order == null)
            {
                returnRequest.CustomerName = order.Customer?.Username;
            }
            else
            {
                order.CustomerName = order.Customer?.Username;
            }
        }

    }
}