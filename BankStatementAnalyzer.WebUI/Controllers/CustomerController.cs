using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.WebUI.Common.constants;
using BankStatementAnalyzer.WebUI.Common.logger;
using BankStatementAnalyzer.WebUI.Filters;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BankStatementAnalyzer.WebUI.Controllers
{
    [RouteAuthorize]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly ICustomerCreditMappingService customerCreditMappingService;
        private readonly IAddressService addressService;
        private readonly IOrderService orderService;

        public CustomerController(ICustomerService customerService,
            ICustomerCreditMappingService customerCreditMappingService,
            ILoggerFacade<BaseController> logger,
            IUserMasterService userMasterService,
            IAddressService addressService,
            IOrderService orderService) : base(userMasterService, logger)
        {
            this.customerService = customerService;
            this.customerCreditMappingService = customerCreditMappingService;
            this.addressService = addressService;
            this.orderService = orderService;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View(customerService.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult AssignCredits(int id)
        {
            var customerCredit = customerCreditMappingService.FindBy(x => x.CustomerId == id).FirstOrDefault();

            if (customerCredit != null)
            {
                var customer = customerService.FindBy(x => x.Id == customerCredit.CustomerId).FirstOrDefault();

                customerCredit.Status = customer.IsCreditApplicable;
            }

            return View(customerCredit);
        }


        [HttpPost]
        public ActionResult AssignCredits(int id, CustomerCreditMapping customerCreditMapping)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning-message"] = ConstantValue.MODEL_STATE_WARNING_MESSAGE;

                return View(customerCreditMapping);
            }

            var oldRecord = customerCreditMappingService.FindBy(x => x.CustomerId == id).FirstOrDefault();

            var customer = customerService.FindBy(x => x.Id == id).FirstOrDefault();

            customer.IsCreditApplicable = customerCreditMapping.Status;
            customerService.Edit(customer);
            customerService.Save();

            if (oldRecord != null)
            {


                oldRecord.AdvancePayment = customerCreditMapping.AdvancePayment;
                oldRecord.CreditAmount = customerCreditMapping.CreditAmount;
                oldRecord.UpdatedBy = UserName;
                oldRecord.UpdatedOn = DateTime.Now;

                customerCreditMappingService.Edit(oldRecord);
                customerCreditMappingService.Save();

                TempData["success-message"] = ConstantValue.MASTER_EDIT_SUCCSESS_MESSAGE;

                return RedirectToAction("Index");
            }

            customerCreditMapping.CustomerId = id;
            customerCreditMapping.CreatedBy = UserName;
            customerCreditMapping.CreatedOn = DateTime.Now;

            customerCreditMappingService.Add(customerCreditMapping);
            customerCreditMappingService.Save();

            TempData["success-message"] = ConstantValue.MASTER_CREATE_SUCCSESS_MESSAGE;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Addresses(int customerId, int orderId)
        {
            if (Session["TempOrderId"] == null)
            {
                Session.Add("TempOrderId", orderId);
            }

            if (Session["customerId"] == null)
            {
                Session.Add("customerId", customerId);
            }
            return View(addressService.FindBy(x => x.CustomerID == customerId && x.Status == true).ToList());
        }

        [HttpPost]
        public ActionResult MapAddress(int addressId)
        {
            var orderId = Convert.ToInt32(Session["TempOrderId"]);
            var order = orderService.FindBy(x => x.ID == orderId).FirstOrDefault();

            order.UpdatedBy = UserName;
            order.UpdatedDate = DateTime.Now.Date;
            order.AddressID = addressId;

            orderService.Edit(order);
            orderService.Save();

            Session.Remove("TempOrderId");
            Session.Remove("customerId");

            TempData["success-message"] = "Order address mapped successfully.";

            return RedirectToAction("WhatsApp", "Orders");
        }

        [HttpPost]
        public ActionResult AddressCreate(Address address)
        {
            var orderId = Convert.ToInt32(Session["TempOrderId"]);
            var customerId = Convert.ToInt32(Session["customerId"]);

            address.CustomerID = customerId;
            address.CreatedBy = UserName;
            address.CreatedDate = DateTime.Now;

            addressService.Add(address);
            addressService.Save();

            TempData["success-message"] = "Address created successfully, Kindly map address with order.";
            return RedirectToAction("Addresses", new { customerId, orderId });
        }
    }
}