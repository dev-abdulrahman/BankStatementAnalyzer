using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.ViewModels;
using BankStatementAnalyzer.WebUI.ViewModels.ReportVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    public class ReportsController : BaseController
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
        private readonly IOrderTypeService orderTypeService;

        public ReportsController(IUserMasterService userMasterService,
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
            IPackageService packageService,
            IOrderTypeService orderTypeService
            ) : base(userMasterService, logger)
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
            this.orderTypeService = orderTypeService;
        }
        // GET: Reports
        public ActionResult OrderReport()
        {

            var statusList = from OrderStatus status in Enum.GetValues(typeof(OrderStatus))
                           select new SelectListItem
                           {
                               Value = ((int)status).ToString(),
                               Text = status.ToString()
                           };

            var orderReportFilterModel = new OrderReportFilterModel()
            {
                EndDate = DateTime.Now.AddMonths(1),
                StartDate = DateTime.Now,
                StatusList = statusList
            };

            return View(orderReportFilterModel);
        }

        [HttpPost]
        public ActionResult OrderReport(OrderReportFilterModel model)
        {
            OrderReportVM orderReportVM = new OrderReportVM();
            var filteredOrderList = (from order in orderService.GetAll().Where(x => x.OrderStatus == model.SelectedStatus && x.CreatedDate >= model.StartDate.Date && x.CreatedDate <= model.EndDate.Date).ToList()                                                                          
                                     select new OrderReportVM()
                                     {
                                         Address = $"{order.Address.Name}|{order.Address.Flat},{order.Address.Area},{order.Address.LandMark},{order.Address.DeliveryAddress}",
                                         AdvancedAmount = order.AdvanceAmount,
                                         BalanceAmount = order.BalanceAmount,
                                         CustomerName = order.Customer.Username,
                                         OrderDate = order.CreatedDate,
                                         OrderNumber = order.ID,
                                         OrderStatus = Enum.GetName(typeof(OrderStatus), order.OrderStatus),
                                         OrderTotal = order.Total,
                                         OrderType = order.OrderType.Name,
                                         PaidByCustomer = order.PaidAmount,
                                         PaymentStatus = order.PaymentStatus,
                                         PaymentToVendor = (decimal)order.PaymentToVendor,
                                         PaymentType = Enum.GetName(typeof(PaymentType), order.PaymentType),
                                         PhoneNumber = order.Customer.PhoneNumber,
                                         Remark = order.Remark,
                                         Tip = (decimal)order.Tip
                                     }).ToList();

            CSVCustomHelper helper = new CSVCustomHelper();
            
            byte[] fileBytes= helper.GetFileBytesByModel(filteredOrderList);

            var now = DateTime.Now;
            var filename = string.Format("Orders_Report_For_{6}_({7})_to_({8})_{0}{1}{2}{3}{4}{5}.csv",
                            now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, Enum.GetName(typeof(OrderStatus), model.SelectedStatus),model.StartDate.ToString("dd-MMM-yyyy"), model.EndDate.ToString("dd-MMM-yyyy"));
            
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            return File(fileBytes, "text/csv");
        }

        public ActionResult ProfitAndLoss()
        {
            var pnlReportFilter = new ProfitAndLossReportFilterModel()
            {
                EndDate = DateTime.Now.AddMonths(1),
                StartDate = DateTime.Now                
            };

            return View(pnlReportFilter);
        }


    }
}