using Microsoft.Owin;
using Owin;
using Autofac;

[assembly: OwinStartupAttribute(typeof(Learn.Web.Startup))]
namespace Learn.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
