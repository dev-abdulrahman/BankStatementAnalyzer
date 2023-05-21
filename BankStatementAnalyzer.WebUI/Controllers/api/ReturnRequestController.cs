//using BankStatementAnalyzer.BusinessLogic.Interface;
//using BankStatementAnalyzer.Models;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace BankStatementAnalyzer.WebUI.Controllers.api
//{
//    [AllowAnonymous]
//    public class ReturnRequestController : ApiController
//    {
//        private readonly IOrderService orderService;
//        private readonly IReturnRequestService returnRequestService;
//        private readonly ICustomerService customerService;
//        private readonly ICompanyService companyService;
//        private readonly ISystemSettingService systemSettingService;
//        private readonly IAddressService addressService;

//        public ReturnRequestController(IOrderService orderService,
//        IReturnRequestService returnRequestService,
//        ICompanyService companyService,
//        ICustomerService customerService,
//        ISystemSettingService systemSettingService,
//        IAddressService addressService)
//        {
//            this.returnRequestService = returnRequestService;
//            this.orderService = orderService;
//            this.companyService = companyService;
//            this.customerService = customerService;
//            this.systemSettingService = systemSettingService;
//            this.addressService = addressService;
//        }

//        [Route("api/ReturnRequest/Create")]
//        public HttpResponseMessage Create(ReturnReq returnReq)
//        {
//            var company = companyService.FindBy(x => x.Key == returnReq.CompanyKey).FirstOrDefault();

//            if (company == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
//            }

//            var user = customerService.FindBy(x => x.UID == returnReq.CustomerUID).FirstOrDefault();

//            if (user == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
//            }

//            if (returnReq.Quantity == 0)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Qunatity can not be 0." });
//            }

//            if (string.IsNullOrWhiteSpace(returnReq.Remark))
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Remark required." });
//            }

//            var address = addressService.FindBy(x => x.ID == returnReq.AddressId && x.Status == true).FirstOrDefault();
//            if (address == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Address details not found." });
//            }

//            if (returnReq.ReturnRequestType == ReturnRequestType.Empty)
//            {
//                ReturnRequest model = new ReturnRequest
//                {
//                    ReturnRequestType = ReturnRequestType.Empty,
//                    NoOfCans = returnReq.Quantity,
//                    Remark = returnReq.Remark,
//                    AddressId = address.ID,
//                    CustomerUID = returnReq.CustomerUID,
//                    OrderStatus = Status.Created
//                };

//                returnRequestService.Add(model);
//                returnRequestService.Save();
//            }

//            if (returnReq.ReturnRequestType == ReturnRequestType.Filled)
//            {
//                var sysVal = systemSettingService.FindBy(x => x.SettingType == SettingType.NewCanReturnRequestMaxDays).FirstOrDefault();
//                var date = sysVal != null ? DateTime.Now.AddDays(-Convert.ToInt16(sysVal.Value)).Date : DateTime.Now.AddDays(-7).Date;

//                var validationResult = validateIfUserHasDeliveredOrderInPast(returnReq.Quantity, orderService.FindBy(x => x.OrderStatus == OrderStatus.Completed && x.UpdatedDate >= date).ToList());

//                if (validationResult.Item1)
//                {
//                    ReturnRequest model = new ReturnRequest
//                    {
//                        ReturnRequestType = ReturnRequestType.Filled,
//                        NoOfCans = returnReq.Quantity,
//                        Remark = returnReq.Remark,
//                        AddressId = address.ID,
//                        CustomerUID = returnReq.CustomerUID,
//                        OrderStatus = Status.Created
//                    };

//                    returnRequestService.Add(model);
//                    returnRequestService.Save();
//                }
//                else if (validationResult.Item2 > 0)
//                {
//                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = $"Quantity can not exceed more than {validationResult.Item2}." }); ;
//                }
//                else
//                {
//                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Return request can not be placed kinldy contact support team." }); ;
//                }
//            }

//            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Return request placed successfully." });
//        }

//        private Tuple<bool, int> validateIfUserHasDeliveredOrderInPast(int quantity, List<Order> orders)
//        {
//            int totalQuantity = 0;
//            foreach (var order in orders)
//            {
//                totalQuantity += order.OrderDetails.Sum(x => x.NewCanQuantity);
//            }

//            if (totalQuantity >= quantity)
//            {
//                return new Tuple<bool, int>(true, totalQuantity);
//            }

//            return new Tuple<bool, int>(false, totalQuantity);
//        }

//        [Route("api/ReturnRequest/Get")]
//        public HttpResponseMessage Get(string customerUID, string companyKey)
//        {
//            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

//            if (company == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
//            }

//            var user = customerService.FindBy(x => x.UID == customerUID).FirstOrDefault();

//            if (user == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
//            }

//            var linqTake = ConfigurationManager.AppSettings["linqTake"];
//            var convertlinqTake = string.IsNullOrWhiteSpace(linqTake) ? 10 : Convert.ToInt32(linqTake);
//            var returnRequest = returnRequestService.FindBy(x => x.CustomerUID == customerUID).OrderByDescending(x => x.ID).Take(convertlinqTake).ToList();
//            var orders = orderService.FindBy(x => x.CustomerID == user.Id && x.OrderStatus == OrderStatus.Completed).ToList();

//            var EmptyCansQuantity = 0;
//            foreach (var order in orders)
//            {
//                var orderDetail = order.OrderDetails.Find(x => x.OrderStatus == OrderStatus.Completed);

//                if (orderDetail != null)
//                {
//                    EmptyCansQuantity += orderDetail.NewCanQuantity;
//                }
//            }

//            List<ReturnReq> respone = new List<ReturnReq>();
//            foreach (var item in returnRequest)
//            {
//                ReturnReq returnReq = new ReturnReq
//                {
//                    Quantity = item.NoOfCans,
//                    Remark = item.Remark,
//                    ReturnRequestTyp = item.ReturnRequestType.ToString(),
//                    Status = item.OrderStatus.ToString()
//                };
//                respone.Add(returnReq);
//            }
//            return Request.CreateResponse(HttpStatusCode.OK, new { respone, EmptyCansQuantity });
//        }

//        public class ReturnReq
//        {
//            public string CompanyKey { get; set; }

//            public string CustomerUID { get; set; }

//            public int Quantity { get; set; }

//            public ReturnRequestType ReturnRequestType { get; set; }

//            public string Remark { get; set; }

//            public int AddressId { get; set; }

//            public string Status { get; set; }

//            public string ReturnRequestTyp { get; set; }
//        }
//    }
//}