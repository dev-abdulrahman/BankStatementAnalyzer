using BankStatementAnalyzer.BusinessLogic.Interface;
using System.Configuration;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class AppConfigurationService : IAppConfigurationService
    {
        public string PayUkey => GetConfiguration("PayUkey");

        public string PayUSalt => GetConfiguration("PayUSalt");

        public string PayULink => GetConfiguration("PayULink");

        public string AdminBaseUrl => GetConfiguration("admin:BaseUrl");

        public string Partner => GetConfiguration("partner");

        public string FromMail => GetConfiguration("fromMail");

        public string AdminEmail => GetConfiguration("adminEmail");

        public string AdminName => GetConfiguration("adminName");

        public string GetConfiguration(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}