using Autofac;
using Autofac.Integration.Mvc;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.SuperAdmin.Common.IoC;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace BankStatementAnalyzer.SuperAdmin.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            var configuration = GlobalConfiguration.Configuration;
            // Register dependencies in controllers

            var assembly = Assembly.Load("BankStatementAnalyzer.SuperAdmin");
            builder.RegisterControllers(assembly);

            //Registring api controller

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            builder.Register<String>(c => Guid.NewGuid().ToString())
               .Named<String>("correlationId")
               .InstancePerRequest();

            // Register our Data dependencies
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());

            builder.RegisterType<BankStatementAnalyzerContext>().InstancePerLifetimeScope();

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}