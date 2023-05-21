using log4net;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankStatementAnalyzer.WebUI.Startup))]
namespace BankStatementAnalyzer.WebUI
{
    public partial class Startup
    {
        public static log4net.ILog Logger { get; set; }

        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger = LogManager.GetLogger(System.Environment.MachineName);

            ConfigureAuth(app);
        }
    }
}