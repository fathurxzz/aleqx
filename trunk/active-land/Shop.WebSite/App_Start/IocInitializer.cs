using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Shop.DataAccess.EntityFramework;


namespace Shop.WebSite
{
    public class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ShopContext>().As<ShopContext>().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
        }
    }
}