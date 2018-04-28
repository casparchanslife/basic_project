using Microsoft.Owin;
using Owin;
using Autofac;

[assembly: OwinStartupAttribute(typeof(Learn.CMS.Startup))]
namespace Learn.CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
