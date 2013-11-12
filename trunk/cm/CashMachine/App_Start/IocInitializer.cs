using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CashMachine.DataAccess;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;

namespace CashMachine.App_Start
{
    public static class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<CardStore>().As<ICardStore>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CardException).Assembly)
                .Where(t => t.GetInterfaces().Contains(typeof(IRepository)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            IContainer container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            var apiResolver = new AutofacWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(resolver);
            GlobalConfiguration.Configuration.DependencyResolver = apiResolver;

        }
    }
}