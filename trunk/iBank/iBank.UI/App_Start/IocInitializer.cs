using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
//using Autofac.Integration.WebApi;
using iBank.Api.Exceptions;
using iBank.DataAccess;
using iBank.DataAccess.EntityFramework;
using iBank.DataAccess.Repositories;
using System.Linq;

namespace iBank.UI
{
    public class IocInitializer
    {
        public static void InitializeIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<BankStore>().As<IBankStore>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof( SecurityException).Assembly)
                .Where(t => t.GetInterfaces().Contains(typeof(IRepository)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            IContainer container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            //var apiResolver = new AutofacWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(resolver);
            //GlobalConfiguration.Configuration.DependencyResolver = apiResolver;
        } 
    }
}