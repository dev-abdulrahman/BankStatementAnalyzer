using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.BusinessLogic.ViewModels;
using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ManageOrderService : IManageOrderService
    {
        private readonly IOrderService orderService;
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;
        private readonly IDeliveryBoyService deliveryBoyService;
        private readonly IOrderDelvieryBoyMappingService orderDelvieryBoyMappingService;
        private readonly ICouponService couponService;
        private readonly ITimeSlotService timeSlotService;
        private readonly ISystemSettingService systemSettingService;
        private readonly IProductService productService;
        private readonly ICustomerCreditMappingService customerCreditMappingService;
        private readonly IReturnRequestService returnRequestService;
        private readonly IReturnRequestDeliveryBoyMappingService returnRequestDeliveryBoyMappingService;
        private readonly IUserMasterService userMasterService;
        private readonly IPackageService packageService;
        private readonly IOrderStatusMappingService orderStatusMappingService;
        private readonly IAppMessageService appMessageService;
        private readonly string linTake;

        public ManageOrderService(IOrderService orderService,
            ICustomerService customerService,
            IAddressService addressService,
            IDeliveryBoyService deliveryBoyService,
            IOrderDelvieryBoyMappingService orderDelvieryBoyMappingService,
            ICouponService couponService,
            ITimeSlotService timeSlotService,
            ISystemSettingService systemSettingService,
            IProductService productService,
            ICustomerCreditMappingService customerCreditMappingService,
            IReturnRequestService returnRequestService,
            IReturnRequestDeliveryBoyMappingService returnRequestDeliveryBoyMappingService,
            IUserMasterService userMasterService,
            IPackageService packageService,
            IOrderStatusMappingService orderStatusMappingService,
            IAppMessageService appMessageService)
        {
            this.orderService = orderService;
            this.customerService = customerService;
            this.addressService = addressService;
            this.deliveryBoyService = deliveryBoyService;
            this.orderDelvieryBoyMappingService = orderDelvieryBoyMappingService;
            this.couponService = couponService;
            this.timeSlotService = timeSlotService;
            this.systemSettingService = systemSettingService;
            this.productService = productService;
            this.customerCreditMappingService = customerCreditMappingService;
            this.returnRequestService = returnRequestService;
            this.returnRequestDeliveryBoyMappingService = returnRequestDeliveryBoyMappingService;
            this.userMasterService = userMasterService;
            this.packageService = packageService;
            this.orderStatusMappingService = orderStatusMappingService;
            this.appMessageService = appMessageService;

            this.linTake = ConfigurationManager.AppSettings["linqTake"];
        }

        public OrderVM GetOrderDetailsByOrderId(int id)
        {
            OrderVM orderVM = new OrderVM();
            Order order = orderService.FindBy(x => x.ID == id && x.OrderStatus != OrderStatus.Removed).FirstOrDefault();
            var currency = appMessageService.FindBy(x => x.Key == "currency").FirstOrDefault();
            orderVM.Currency = currency == null ? "AED" : currency.Message;

            #region Order
            orderVM.OrderType = order.OrderType?.Name;
            orderVM.OrderId = order.ID;
            orderVM.PlacedOn = order.UpdatedDate.Value;
            orderVM.OrderStatus = order.OrderStatus.ToString();
            orderVM.PaymentType = order.PaymentType.ToString();
            orderVM.PaidAmount = order.PaidAmount == 0 ? order.OrderDetails.Sum(x => x.Price * x.Quantity) : order.Total;
            orderVM.OrderTotal = orderVM.PaidAmount;
            #endregion

            #region package
            if (order.OrderType?.Name == "Package")
            {
                var coupon = order.CouponId == 0 ? null : couponService.FindBy(x => x.ID == order.CouponId).FirstOrDefault();
                var package = packageService.FindBy(x => x.ID == order.PackageId.Value).FirstOrDefault();
                orderVM.PackageName = package?.Name;
                if (coupon != null)
                {
                    var couponRate = packageService.GetDiscount(package.Rate, package.UrgentRate, coupon.Discount, coupon.DiscountType);
                    orderVM.Coupon = order.IsUrgent ? package.UrgentRate - couponRate.Item2 : package.Rate - couponRate.Item1;
                    orderVM.OrderTotal = order.IsUrgent ? package.UrgentRate : package.Rate;
                    orderVM.PaidAmount = order.IsUrgent ? couponRate.Item2 : couponRate.Item1;
                }
                else
                {
                    orderVM.OrderTotal = order.IsUrgent ? package.UrgentRate : package.Rate;
                    orderVM.PaidAmount = order.IsUrgent ? package.UrgentRate : package.Rate;
                }
            }
            #endregion

            #region Order details
            foreach (var orderDetail in order.OrderDetails.Where(x => x.OrderStatus == order.OrderStatus))
            {
                OrderSummary orderSummary = new OrderSummary
                {
                    Id = orderDetail.Id,
                    Price = orderDetail.Price,
                    Quantity = orderDetail.Quantity,
                    TotalPayable = orderDetail.Price * orderDetail.Quantity,
                    Service = orderDetail.ServiceType?.Name,
                    Cloth = orderDetail.Cloths?.Name,
                    IsUrgent = orderDetail.IsUrgent,
                    Status = orderDetail.OrderStatus.ToString(),
                    Remark = orderDetail.Remark
                };

                orderVM.OrderSummary.Add(orderSummary);
            }
            #endregion

            #region Customer
            orderVM.Customer.Id = order.Customer.Id;
            orderVM.Customer.Name = order.Customer.Username;
            orderVM.Customer.Mobile = order.Customer.PhoneNumber;
            orderVM.Customer.Email = order.Customer.Email;
            #endregion

            #region Address
            orderVM.Address.Area = order.Address?.Area;
            orderVM.Address.DeliveryAddress = order.Address?.DeliveryAddress;
            orderVM.Address.Flat = order.Address?.Flat;
            orderVM.Address.LandMark = order.Address?.LandMark;
            orderVM.Address.Longitude = order.Address?.Longitude;
            orderVM.Address.Latitude = order.Address?.Latitude;
            orderVM.Address.Pincode = order.Address?.Pincode;
            #endregion

            return orderVM;
        }

        public OrderResponse GetOrderById(int orderId)
        {
            #region Past Orders
            var top = string.IsNullOrWhiteSpace(this.linTake) ? 20 : Convert.ToInt32(this.linTake);
            var date = DateTime.Now.Date;

            var order = orderService.
                       FindBy(x => x.ID == orderId).FirstOrDefault();

            OrderResponse response = mapOrder(order);
            #endregion

            return response;
        }

        public Tuple<List<OrderResponse>, List<OrderResponse>> GetTop20Orders(int customerId)
        {
            List<OrderResponse> pastResponses = new List<OrderResponse>();
            List<OrderResponse> currentResponses = new List<OrderResponse>();

            #region Past Orders
            var top = string.IsNullOrWhiteSpace(this.linTake) ? 20 : Convert.ToInt32(this.linTake);
            var date = DateTime.Now.Date;

            var pastOrders = orderService.
                       FindBy(x => x.CustomerID == customerId && (x.OrderStatus == OrderStatus.Delivered || x.OrderStatus == OrderStatus.Cancelled) && x.UpdatedDate < date)
                      .OrderByDescending(x => x.ID)
                      .Take(top)
                      .ToList();

            foreach (var pastOrder in pastOrders)
            {
                OrderResponse model = mapOrder(pastOrder);

                pastResponses.Add(model);
            }
            #endregion

            #region Current Orders
            var ids = pastOrders.Select(x => x.ID).ToList();

            var currentOrders = orderService.
                         FindBy(x => !ids.Contains(x.ID) && x.CustomerID == customerId && (x.OrderStatus != OrderStatus.Removed) && x.UpdatedDate <= date)
                        .OrderByDescending(x => x.ID)
                        .Take(top)
                        .ToList();

            foreach (var currentOrder in currentOrders)
            {
                OrderResponse model = mapOrder(currentOrder);

                currentResponses.Add(model);
            }
            #endregion

            return new Tuple<List<OrderResponse>, List<OrderResponse>>(pastResponses, currentResponses);
        }

        private OrderResponse mapOrder(Order order)
        {
            var isPackage = false;
            Package package = null;
            List<string> descriptionString = new List<string>();

            if (order.OrderType?.Name == "Package")
            {
                isPackage = true;
                package = packageService.FindBy(x => x.ID == order.PackageId.Value).FirstOrDefault();

                if (package != null)
                {
                    string[] desc = package.Description.Split(',');

                    foreach (var item in desc)
                    {
                        descriptionString.Add(item);
                    }
                }
            }

            OrderResponse orderResponse = new OrderResponse
            {
                Id = order.ID,
                OrderType = order.OrderType?.Name,
                OrderTypeId = order.OrderTypeId,
                OrderStatus = order.OrderStatus.ToString(),
                AddressId = new OrderAddressVM
                {
                    AddressType = order.Address == null ? AddressType.Home : order.Address.AddressType,
                    Area = order.Address == null ? "" : order.Address.Area,
                    Id = order.Address == null ? 0 : order.Address.ID,
                    Flat = order.Address == null ? "" : order.Address.Flat,
                    LandMark = order.Address == null ? "" : order.Address.LandMark,
                    DeliveryAddress = order.Address == null ? "" : order.Address.DeliveryAddress,
                    CustomerId = order.CustomerID,
                    Latitude = order.Address == null ? 0 : order.Address.Latitude,
                    Longitude = order.Address == null ? 0 : order.Address.Longitude,
                    Pincode = order.Address == null ? "" : order.Address.Pincode,
                    Name = order.Address == null ? "" : order.Address.Name
                },
                CreatedOn = order.CreatedDate,
                TotalPayable = order.PaidAmount,
                PromoCodeDiscount = order.Coupon == null ? 0
                                  : isPackage ? order.IsUrgent ? getCouponAmount(order.Coupon, package.UrgentRate)
                                  : getCouponAmount(order.Coupon, package.Rate)
                                  : 0,
                Remark = order.Remark,
                Package = isPackage == false ? null : new OrderPackageVM
                {
                    Id = package.ID,
                    Name = package.Name,
                    Dimension = package.Diemension,
                    Description = descriptionString,
                    IsUrgent = order.IsUrgent,
                    Rate = (int)decimal.Truncate(package.Rate),
                    UrgentRate = (int)decimal.Truncate(package.UrgentRate),
                }
            };


            var orderStatuses = orderStatusMappingService.FindBy(x => x.OrderId == order.ID).ToList();
            foreach (var orderStatus in orderStatuses)
            {
                OrderHistory orderHistory = new OrderHistory
                {
                    CreatedOn = orderStatus.CreatedOn,
                    Status = orderStatus.OrderStatus.ToString(),
                    EstimatedDate = orderStatus.EstimatedDate,
                };

                orderResponse.OrderEstimate.Add(orderHistory);
            }


            foreach (var orderDetail in order.OrderDetails)
            {
                OrderDetailVM orderDetailVM = new OrderDetailVM
                {
                    Cloth = orderDetail.Cloths?.Name,
                    Service = orderDetail.ServiceType?.Name,
                    OrderId = orderDetail.OrderID,
                    IsUrgent = orderDetail.IsUrgent,
                    Price = orderDetail.Price,
                    Quantity = orderDetail.Quantity,
                    TotalPayable = orderDetail.Price * orderDetail.Quantity,
                    Remark = orderDetail.Remark
                };

                orderResponse.OrderDetails.Add(orderDetailVM);
            }

            if (orderResponse.Package != null)
            {
                orderResponse.TotalPayable = order.PaidAmount;
            }
            else
            {
                orderResponse.TotalPayable = orderResponse.OrderDetails.Sum(x => x.TotalPayable);
            }
            return orderResponse;
        }

        private decimal getCouponAmount(Coupon coupon, decimal totalAmount)
        {
            if (coupon == null)
            {
                return 0;
            }

            decimal couponAmount = 0;

            if (coupon.DiscountType == DiscountType.Percent)
            {
                couponAmount = (totalAmount * coupon.Discount) / 100;
            }
            else
            {
                couponAmount = coupon.Discount;
            }

            return couponAmount;
        }
    }
}