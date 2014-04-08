﻿using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using EM2014.Models;


namespace EM2014.App_Start
{
    public static class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<SiteContext>().As<SiteContext>().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);

        }
    }
}