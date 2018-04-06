using Microsoft.Owin;
using Owin;

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
