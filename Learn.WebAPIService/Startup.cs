using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Learn.WebAPIService.Manager;

[assembly: OwinStartup(typeof(Learn.WebAPIService.Startup))]

namespace Learn.WebAPIService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            // token generation
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                Provider = new AuthorizationServerManager(),
                RefreshTokenProvider = new RefreshTokenManager(),
            });

            // token consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
