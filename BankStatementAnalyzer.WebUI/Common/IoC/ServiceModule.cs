using Autofac;
using BankStatementAnalyzer.WebUI.Common.logger;
using System.Linq;
using System.Reflection;

namespace BankStatementAnalyzer.WebUI.Common.IoC
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(LoggerFacade<>)).As(typeof(ILoggerFacade<>))
                .WithParameter((p, c) => p.Name == "correlationId", (p, c) => c.ResolveNamed<string>("correlationId"));

            builder.RegisterTypes(Assembly.Load("BankStatementAnalyzer.BusinessLogic")
                .GetExportedTypes()
                .Where(x => x.Name.EndsWith("Service")).ToArray())
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}