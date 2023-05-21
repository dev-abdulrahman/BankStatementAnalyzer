using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.BusinessLogic.ViewModels;
using BankStatementAnalyzer.Models;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class CartManageService : ICartManageService
    {
        private readonly IProductService productService;
        private readonly IFileMasterService fileMasterService;
        private readonly IManageWalletService manageWalletService;
        private readonly ICouponService couponService;
        private readonly ISystemSettingService systemSettingService;

        public CartManageService(IProductService productService,
             IFileMasterService fileMasterService,
             IManageWalletService manageWalletService,
             ICouponService couponService,
             ISystemSettingService systemSettingService)
        {
            this.couponService = couponService;
            this.productService = productService;
            this.systemSettingService = systemSettingService;
            this.manageWalletService = manageWalletService;
            this.fileMasterService = fileMasterService;
        }

        public List<CartViewModel> Get(List<Order> orders, int customerID, int customerId, string url)
        {
            List<CartViewModel> cartViewModels = new List<CartViewModel>();

            return cartViewModels;
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
