using System.Web.Mvc;
using Autofac;
using System.Reflection;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Autofac.Integration.Mvc;
using Learn.Data.Infrastructure;
using Learn.Lib.Infrastructure.Interface;
using Learn.Data.Infrastructure.Interface;
using Learn.Lib.Infrastructure.Repositories;
using Learn.Web.Service.Services;
using Learn.Data;
using Learn.DataModel.Models;
using Learn.Web;

namespace Learn
{
    public static class AutofacConfig
    {
        public static void Init()
        {
            SetAutofacContainer();
        }
        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(NoteRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(NoteService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
             .InstancePerHttpRequest();
            //builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            //    .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();

            builder.RegisterModule<AutofacWebTypesModule>();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}