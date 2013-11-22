using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using TheSmallSpaceGame.Api.Contracts;
using TheSmallSpaceGame.DataAccess;
using TheSmallSpaceGame.DataAccess.EntityFramework.Models;
using TheSmallSpaceGame.DataAccess.Repositories;

namespace TheSmallSpaceGame.Site.App_Start
{
    public class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<GameStore>().As<IGameStore>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(UserException).Assembly)
                .Where(t => t.GetInterfaces().Contains(typeof(IRepository)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            IContainer container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);

            DependencyResolver.SetResolver(resolver);
        }
    }
}