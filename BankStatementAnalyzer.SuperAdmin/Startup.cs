using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankStatementAnalyzer.SuperAdmin.Startup))]
namespace BankStatementAnalyzer.SuperAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           ConfigureAuth(app);
        }
    }
}