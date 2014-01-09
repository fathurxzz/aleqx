using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AccuV.DataAccess;
using AccuV.DataAccess.Entities;
using AccuV.DataAccess.EntityFramework;
using AccuV.UI.Security;
using AccuVAPI.Operations;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;

namespace AccuV.UI
{
    public static class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<RolloutTrackingContext>().As<IDataSession>().InstancePerLifetimeScope();
            builder.RegisterType<UserStore>().As<IUserStore<User>>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IOperationStore).Assembly)
                .Where(t => t.GetInterfaces().Contains(typeof(IOperationStore)))
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