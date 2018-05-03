using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Learn.CMS;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Microsoft.Owin.Security;
using Learn.Core.Infrastructure;
using Learn.Core.Infrastructure.Interface;
using Learn.Core.Repositories;
using Learn.Core.Services;
using Learn.Core;
using Learn.Core.DataModels;
using Learn.Core.Manager;
using Learn.Core.Attributes;
using Learn.Core.Infrastructure.Repositories;

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

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(NoteRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(ApplicationUserRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(FunctionRoleRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(NoteService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(FunctionRoleService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof(UserRoleService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerHttpRequest();

            builder.RegisterFilterProvider();
            builder.RegisterType<AuthorizeUserAttribute>().PropertiesAutowired();

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => new RoleStore<ApplicationRole>(c.Resolve<ApplicationDbContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });
            builder.Register(c => new IdentityFactoryOptions<ApplicationRoleManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
             .InstancePerHttpRequest();

            //builder.RegisterModule<AutofacWebTypesModule>();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}