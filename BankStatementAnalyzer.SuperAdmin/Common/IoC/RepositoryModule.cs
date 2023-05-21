using Autofac;
using System.Linq;
using System.Reflection;

namespace BankStatementAnalyzer.SuperAdmin.Common.IoC
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypes(Assembly.Load("BankStatementAnalyzer.Repository")
                .GetExportedTypes()
                .Where(x => x.Name.EndsWith("Repository")).ToArray())
                .AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}