using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.SuperAdmin.App_Start;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;

namespace BankStatementAnalyzer.SuperAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.Configure();
            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer<BankStatementAnalyzerContext>(null);
        }
    }
}