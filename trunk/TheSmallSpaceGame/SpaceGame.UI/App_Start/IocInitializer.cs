using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.EntityFramework;
using SpaceGame.DataAccess.Repositories;

namespace SpaceGame.UI
{
    public static class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<SpaceStore>().As<ISpaceStore>().InstancePerLifetimeScope();

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