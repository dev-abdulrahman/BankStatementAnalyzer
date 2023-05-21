//using BankStatementAnalyzer.BusinessLogic.Interface;
//using BankStatementAnalyzer.Models;
//using BankStatementAnalyzer.WebUI.ViewModels.Api;
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
//    public class CartController : ApiController
//    {
//        private readonly ICustomerService customerService;
//        private readonly IOrderService orderService;
//        private readonly IOrderDetailService orderDetailService;
//        private readonly ICompanyService companyService;
//        private readonly IProductService productService;
//        private readonly IFileMasterService fileMasterService;
//        private readonly IManageWalletService manageWalletService;
//        private readonly ICouponService couponService;
//        private readonly ISystemSettingService systemSettingService;
//        private readonly ICartManageService cartManageService;
//        private readonly IOrderStatusMappingService orderStatusMappingService;

//        public CartController(ICompanyService companyService,
//            IOrderDetailService orderDetailService,
//            IOrderService orderService,
//            ICustomerService customerService,
//            IProductService productService,
//            IFileMasterService fileMasterService,
//            IManageWalletService manageWalletService,
//            ICouponService couponService,
//            ISystemSettingService systemSettingService,
//            ICartManageService cartManageService,
//            IOrderStatusMappingService orderStatusMappingService)
//        {
//            this.companyService = companyService;
//            this.customerService = customerService;
//            this.orderService = orderService;
//            this.orderDetailService = orderDetailService;
//            this.productService = productService;
//            this.fileMasterService = fileMasterService;
//            this.manageWalletService = manageWalletService;
//            this.couponService = couponService;
//            this.systemSettingService = systemSettingService;
//            this.cartManageService = cartManageService;
//            this.orderStatusMappingService = orderStatusMappingService;
//        }

//        [HttpGet]
//        [Route("api/Cart/Get")]
//        public HttpResponseMessage Get(string customerId, string companyKey, OrderType orderType)
//        {

//            var company = companyService.FindBy(x => x.Key == companyKey).FirstOrDefault();

//            if (company == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
//            }

//            var user = customerService.FindBy(x => x.UID == customerId).FirstOrDefault();

//            if (user == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
//            }

//            var cartItems = orderService.GetAll().Where(x => x.CustomerID == user.Id && x.OrderStatus == OrderStatus.Draft && x.OrderType == orderType).ToList();

//            if (cartItems.Count == 0)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Cart is empty." });
//            }

//            var resp = cartManageService.Get(cartItems, customerId, user.Id, Request.RequestUri.GetLeftPart(UriPartial.Authority), company.ID);

//            return Request.CreateResponse(HttpStatusCode.OK, resp);
//        }

//        [HttpPost]
//        [Route("api/Cart/Add")]
//        public HttpResponseMessage Add(OrderViewModel orderViewModel)
//        {
//            var company = companyService.FindBy(x => x.Key == orderViewModel.CompanyKey).FirstOrDefault();

//            if (company == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
//            }

//            var user = customerService.FindBy(x => x.UID == orderViewModel.CustomerId).FirstOrDefault();

//            if (user == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
//            }

//            var oldOrder = orderService.FindBy(x => x.CustomerID == user.Id && x.OrderStatus == OrderStatus.Draft && x.OrderType == orderViewModel.OrderType).FirstOrDefault();
//            var prod = productService.FindBy(x => x.ID == orderViewModel.ProductBrandId).FirstOrDefault();

//            if (prod == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Product details not found." });
//            }

//            if (oldOrder != null)
//            {
//                //var quantity = orderDetailService.FindBy(x => x.ProductID == prod.ID && x.OrderStatus == OrderStatus.Draft).FirstOrDefault() != null ?
//                //               orderDetailService.FindBy(x => x.ProductID == prod.ID && x.OrderStatus == OrderStatus.Draft).Sum(x => x.Quantity) : 0;

//                foreach (var item in oldOrder.OrderDetails.Where(x => x.OrderStatus == OrderStatus.Draft && x.ProductID == orderViewModel.ProductBrandId))
//                {
//                    //if (prod.Quantity == quantity)
//                    //{
//                    //    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = $"Product quantity can not exceed more than {prod.Quantity}." });
//                    //}
//                    //else if (prod.Quantity > quantity)
//                    //{
//                    item.Quantity += 1;

//                    orderDetailService.Edit(item);
//                    orderDetailService.Save();
//                    //}
//                }

//                if (oldOrder.OrderDetails.Where(x => x.ProductID == orderViewModel.ProductBrandId).Count() == 0)
//                {
//                    //if (prod.Quantity < orderViewModel.Quantity)
//                    //{
//                    //    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = $"Product quantity can not exceed more than {prod.Quantity}." });
//                    //}

//                    OrderDetail orderDetail1 = new OrderDetail
//                    {
//                        OrderID = oldOrder.ID,
//                        ContactNo = user.PhoneNumber,
//                        Description = prod.Description,
//                        IsReorder = false,
//                        OrderStatus = OrderStatus.Draft,
//                        ProductID = orderViewModel.ProductBrandId,
//                        Price = prod.Price,
//                        Quantity = orderViewModel.Quantity,
//                        TotalSavings = 0,
//                        DiscountedPercentage = 0
//                    };

//                    orderDetailService.Add(orderDetail1);
//                    orderDetailService.Save();
//                }
//                else if (oldOrder.OrderDetails.Where(x => x.ProductID == orderViewModel.ProductBrandId && x.OrderStatus == OrderStatus.Removed).Count() != 0)
//                {
//                    OrderDetail orderDetail1 = new OrderDetail
//                    {
//                        OrderID = oldOrder.ID,
//                        ContactNo = user.PhoneNumber,
//                        Description = prod.Description,
//                        IsReorder = false,
//                        OrderStatus = OrderStatus.Draft,
//                        ProductID = orderViewModel.ProductBrandId,
//                        Price = prod.Price,
//                        Quantity = orderViewModel.Quantity,
//                        TotalSavings = 0,
//                        DiscountedPercentage = 0
//                    };

//                    orderDetailService.Add(orderDetail1);
//                    orderDetailService.Save();
//                }
//            }
//            else
//            {
//                Order order = new Order
//                {
//                    CustomerID = user.Id,
//                    OrderStatus = OrderStatus.Draft,
//                    Total = 0,
//                    PaymentType = PaymentType.COD,
//                    CreatedBy = "API",
//                    OrderType = orderViewModel.OrderType,
//                    UniqueOrderNo = getUniqueOrderNumer()
//                };

//                if (prod != null)
//                {
//                    if (prod.Quantity >= orderViewModel.Quantity)
//                    {
//                        OrderDetail orderDetail1 = new OrderDetail
//                        {
//                            ContactNo = user.PhoneNumber,
//                            Description = prod.Description,
//                            IsReorder = false,
//                            OrderStatus = OrderStatus.Draft,
//                            ProductID = orderViewModel.ProductBrandId,
//                            Price = prod.Price,
//                            Quantity = orderViewModel.Quantity,
//                            TotalSavings = 0,
//                            DiscountedPercentage = 0

//                        };

//                        order.OrderDetails.Add(orderDetail1);
//                    }
//                    else
//                    {
//                        return Request.CreateResponse(HttpStatusCode.NotFound, new { message = $"Product quantity can not exceed more than {prod.Quantity}." });
//                    }
//                }

//                orderService.Add(order);
//                orderService.Save();

//                OrderStatusMapping orderStatusMapping = new OrderStatusMapping
//                {
//                    CreatedOn = DateTime.Now,
//                    OrderId = order.ID,
//                    OrderStatus = OrderStatus.Draft
//                };

//                orderStatusMappingService.Add(orderStatusMapping);
//                orderStatusMappingService.Save();
//            }

//            return Request.CreateResponse(HttpStatusCode.OK, new { message = "Created successfully." });
//        }

//        private string getUniqueOrderNumer()
//        {
//            var vstConfig = ConfigurationManager.AppSettings["companyCode"];
//            var orderNo = "";

//            var order = orderService.GetAll().OrderByDescending(x => x.ID).FirstOrDefault();
//            if (order == null)
//            {
//                return orderNo = $"{vstConfig}-0001";
//            }
//            else
//            {
//                var dbOrder = order.UniqueOrderNo.Split('-');
//                var seq = Convert.ToInt32(dbOrder[1]);
//                var final = seq + 1;
//                return orderNo = $"{vstConfig}-{final.ToString("D4")}";
//            }
//        }

//        [Route("api/Cart/PlusMinuQuantity")]
//        public HttpResponseMessage PlusMinuQuantity(UpdateQuantity updateQuantity)
//        {
//            var company = companyService.FindBy(x => x.Key == updateQuantity.CompanyKey).FirstOrDefault();

//            if (company == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
//            }

//            if (updateQuantity.IsplusQuantity)
//            {
//                var orderDetail = orderDetailService.FindBy(x => x.Id == updateQuantity.OrderDetailId).FirstOrDefault();

//                if (orderDetail == null)
//                {
//                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Order details not found." });

//                }

//                //var prodDetail = productService.FindBy(x => x.ID == updateQuantity.ProductId).FirstOrDefault();

//                //bool quantity = validateQunatity(prodDetail);

//                //if (!quantity)
//                //{
//                //    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Out of stock." });
//                //}

//                orderDetail.Quantity += updateQuantity.Quantity;

//                orderDetailService.Edit(orderDetail);
//                orderDetailService.Save();

//                #region Coupon remove
//                var model = orderService.FindBy(x => x.ID == orderDetail.OrderID).FirstOrDefault();
//                if (model.CouponId > 0)
//                {
//                    model.CouponId = 0;
//                    orderService.Edit(model);
//                    orderService.Save();
//                }
//                #endregion

//                //return Request.CreateResponse(HttpStatusCode.OK, new { message = "Quantity updated successfully." });
//            }
//            else
//            {
//                var orderDetail = orderDetailService.FindBy(x => x.Id == updateQuantity.OrderDetailId).FirstOrDefault();

//                if (orderDetail == null)
//                {
//                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Order details not found." });

//                }

//                var order = orderService.FindBy(x => x.ID == orderDetail.OrderID).FirstOrDefault();

//                orderDetail.Quantity -= updateQuantity.Quantity;

//                if (orderDetail.Quantity == 0)
//                {
//                    orderDetail.OrderStatus = OrderStatus.Removed;
//                }

//                orderDetailService.Edit(orderDetail);
//                orderDetailService.Save();

//                if (order.OrderDetails.Where(x => x.OrderStatus == OrderStatus.Draft).Count() < 1)
//                {
//                    order.CouponId = 0;
//                    order.OrderStatus = OrderStatus.Removed;
//                    orderDetailService.Edit(orderDetail);
//                    orderDetailService.Save();
//                }

//                #region Coupon remove
//                var model = orderService.FindBy(x => x.ID == orderDetail.OrderID).FirstOrDefault();
//                if (model.CouponId > 0)
//                {
//                    model.CouponId = 0;
//                    orderService.Edit(model);
//                    orderService.Save();
//                }
//                #endregion

//                //return Request.CreateResponse(HttpStatusCode.OK, new { message = "Quantity updated successfully." });
//            }

//            var user = customerService.FindBy(x => x.UID == updateQuantity.CustomerId).FirstOrDefault();

//            if (user == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
//            }

//            var cartItems = orderService.GetAll().Where(x => x.CustomerID == user.Id && x.OrderStatus == OrderStatus.Draft).ToList();

//            if (cartItems.Count == 0)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Cart is empty." });
//            }

//            var resp = cartManageService.Get(cartItems, updateQuantity.CustomerId, user.Id, Request.RequestUri.GetLeftPart(UriPartial.Authority), company.ID);

//            return Request.CreateResponse(HttpStatusCode.OK, new { Cart = resp });
//        }

//        [Route("api/Cart/Remove")]
//        public HttpResponseMessage Remove(RemoveOrderDetail removeOrderDetail)
//        {
//            var company = companyService.FindBy(x => x.Key == removeOrderDetail.CompanyKey).FirstOrDefault();

//            if (company == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Company details not found." });
//            }

//            var orderDetail = orderDetailService.FindBy(x => x.Id == removeOrderDetail.OrderDetailId).FirstOrDefault();

//            if (orderDetail == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Order details not found." });

//            }

//            var order = orderService.FindBy(x => x.ID == orderDetail.OrderID).FirstOrDefault();

//            orderDetail.OrderStatus = OrderStatus.Removed;

//            orderDetailService.Edit(orderDetail);
//            orderDetailService.Save();


//            if (order.OrderDetails.Where(x => x.OrderStatus == OrderStatus.Draft).Count() < 1)
//            {
//                order.CouponId = 0;
//                order.OrderStatus = OrderStatus.Removed;
//                orderService.Edit(order);
//                orderService.Save();
//            }

//            #region Coupon remove
//            if (order.CouponId > 0)
//            {
//                order.CouponId = 0;
//                orderService.Edit(order);
//                orderService.Save();
//            }
//            #endregion

//            var user = customerService.FindBy(x => x.UID == removeOrderDetail.CustomerId).FirstOrDefault();

//            if (user == null)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Invalid customer details." });
//            }

//            var cartItems = orderService.GetAll().Where(x => x.CustomerID == user.Id && x.OrderStatus == OrderStatus.Draft).ToList();

//            if (cartItems.Count == 0)
//            {
//                return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Cart is empty." });
//            }

//            var resp = cartManageService.Get(cartItems, removeOrderDetail.CustomerId, user.Id, Request.RequestUri.GetLeftPart(UriPartial.Authority), company.ID);

//            return Request.CreateResponse(HttpStatusCode.OK, new { Cart = resp });

//            //return Request.CreateResponse(HttpStatusCode.OK, new { message = "Removed successfully." });
//        }

//        private bool validateQunatity(Product prodDetail)
//        {
//            var brandQuantity = orderDetailService.FindBy(x => x.ProductID == prodDetail.ID).FirstOrDefault() != null ?
//                                orderDetailService.FindBy(x => x.ProductID == prodDetail.ID).Sum(x => x.Quantity) : 0;

//            if (prodDetail.Quantity > brandQuantity)
//            {
//                return true;
//            }

//            return false;
//        }
//    }
//}
