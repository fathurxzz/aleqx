using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Linq;
using Kiki.Api;
using Kiki.DataAccess;
using Kiki.DataAccess.Models;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite
{
    public class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<SiteStore>().As<ISiteStore>().InstancePerLifetimeScope();

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