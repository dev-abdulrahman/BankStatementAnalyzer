using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.BusinessLogic.ViewModels;
using System;
using System.Configuration;
using System.Linq;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class DashboardService : IDashboardService
    {
        private readonly IOrderService orderService;
        private readonly int customize;
        private readonly int oneTouch;
        private readonly int whatsApp;
        private readonly int package;
        private readonly string baseURL;

        public DashboardService(IOrderService orderService)
        {
            this.orderService = orderService;
            customize = Convert.ToInt32(ConfigurationManager.AppSettings["customize"]);
            oneTouch = Convert.ToInt32(ConfigurationManager.AppSettings["oneTouch"]);
            whatsApp = Convert.ToInt32(ConfigurationManager.AppSettings["whatsApp"]);
            package = Convert.ToInt32(ConfigurationManager.AppSettings["package"]);
            baseURL = ConfigurationManager.AppSettings["baseURL"];
        }
        public Dashboard GetCustomizeCount()
        {
            var orderCount = orderService.FindBy(x => x.OrderTypeId == customize && x.OrderStatus == Models.OrderStatus.Ordered).Count();
            Dashboard dashboard = new Dashboard
            {
                Count = orderCount,
                FavIcon = "fa fa-pencil-square-o",
                OrderType = "Customize",
                IconColor = "purple",
                URL = $"{baseURL}Orders/Customize"
            };

            return dashboard;
        }

        public Dashboard GetOneTouchCount()
        {
            var orderCount = orderService.FindBy(x => x.OrderTypeId == oneTouch && x.OrderStatus == Models.OrderStatus.Ordered).Count();
            Dashboard dashboard = new Dashboard
            {
                Count = orderCount,
                FavIcon = "fa fa-hand-o-up",
                OrderType = "One Touch",
                IconColor = "success",
                URL = $"{baseURL}Orders/OneTouch"
            };

            return dashboard;
        }

        public Dashboard GetPackageCount()
        {
            var orderCount = orderService.FindBy(x => x.OrderTypeId == package && x.OrderStatus == Models.OrderStatus.Ordered).Count();
            Dashboard dashboard = new Dashboard
            {
                Count = orderCount,
                FavIcon = "fa fa-shopping-bag",
                OrderType = "Package",
                IconColor = "red",
                URL = $"{baseURL}Orders/Package"
            };

            return dashboard;
        }

        public Dashboard GetWhatsAppCount()
        {
            var orderCount = orderService.FindBy(x => x.OrderTypeId == whatsApp && x.OrderStatus == Models.OrderStatus.Ordered).Count();
            Dashboard dashboard = new Dashboard
            {
                Count = orderCount,
                FavIcon = "fa fa-whatsapp",
                OrderType = "WhatsApp",
                IconColor = "green",
                URL = $"{baseURL}Orders/WhatsApp"
            };

            return dashboard;
        }
    }
}
