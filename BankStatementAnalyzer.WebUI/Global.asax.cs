using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.WebUI.App_Start;
using BankStatementAnalyzer.WebUI.ViewModels.Datatables;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BankStatementAnalyzer.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.Configure();
            AutofacConfig.ConfigureContainer(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<BankStatementAnalyzerContext>(null);

            ModelBinders.Binders.Add(typeof(DataTablesPageRequest), new DataTablesModelBinder());
        }
    }
}