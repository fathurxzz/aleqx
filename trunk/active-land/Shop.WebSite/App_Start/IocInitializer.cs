using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Linq;

using Shop.Api.Contracts.Exceptions;
using Shop.DataAccess;
using Shop.DataAccess.EntityFramework;
using Shop.DataAccess.Repositories;


namespace Shop.WebSite
{
    public class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ShopStore>().As<IShopStore>().InstancePerLifetimeScope();

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