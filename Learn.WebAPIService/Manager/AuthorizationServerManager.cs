using Learn.Lib;
using Learn.Data;
using Learn.DataModel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Learn.WebAPIService.Manager
{
    public class AuthorizationServerManager : OAuthAuthorizationServerProvider
    {
        private UserManager<ApplicationUser> _userManager;

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId, clientSecret;
            if (
                    context.TryGetBasicCredentials(out clientId, out clientSecret) &&
                    (
                        (clientId == OAuthGlobal.OAuth_Client_Id_iOS && clientSecret == OAuthGlobal.OAuth_Client_Secret)
                        ||
                        (clientId == OAuthGlobal.OAuth_Client_Id_Android && clientSecret == OAuthGlobal.OAuth_Client_Secret)
                    )
                )
            {
                context.Validated();
            }
            else
            {
                context.SetError("invalid_credentials");
                context.Rejected();
            }
            return Task.FromResult<object>(null);
        }

        public override async Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            string username, password;
            AuthenticationProperties origionalProperties = new AuthenticationProperties();
            ClaimsIdentity origionalIdentity = new ClaimsIdentity("Bearer");

            origionalProperties.Dictionary.Add("client_id", context.ClientId);
            if (String.IsNullOrEmpty(context.Parameters.Get("deviceId")))
            {
                origionalIdentity.AddClaim(new Claim("deviceId", Guid.NewGuid().ToString()));
            }
            else
            {
                origionalIdentity.AddClaim(new Claim("deviceId", context.Parameters.Get("deviceId")));
            }

            origionalIdentity.AddClaim(new Claim("since", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            origionalIdentity.AddClaim(new Claim(ClaimTypes.Role, context.GrantType));

            username = String.IsNullOrEmpty(context.Parameters.Get("username")) ? "" : context.Parameters.Get("username");
            password = String.IsNullOrEmpty(context.Parameters.Get("password")) ? "" : context.Parameters.Get("password");

            var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            IdentityUser user = await _userManager.FindAsync(username, password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            context.Validated(new AuthenticationTicket(origionalIdentity, origionalProperties));


            origionalIdentity.AddClaim(new Claim("accountName", username));
            //identity.AddClaim(new Claim("role", "user"));
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            if (
                (context.ClientId == OAuthGlobal.OAuth_Client_Id_iOS && context.Ticket.Properties.Dictionary["client_id"] == OAuthGlobal.OAuth_Client_Id_iOS)
                ||
                (context.ClientId == OAuthGlobal.OAuth_Client_Id_Android && context.Ticket.Properties.Dictionary["client_id"] == OAuthGlobal.OAuth_Client_Id_Android)
                )
            {
                var ticket = new AuthenticationTicket(context.Ticket.Identity, context.Ticket.Properties);
                context.Validated(ticket);
            }
            else

                context.SetError("invalid_credentials");
            context.Rejected();
            //return;
            return Task.FromResult<object>(null);
        }

        private static List<string> AdditionalResponseParametersKeyList = new List<string>() {
            "deviceId",
            "status",
            "since",
            "accountName",
        };
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            //foreach (var property in context.Identity.Claims.Where(o => AdditionalResponseParametersKeyList.Contains(o.Type)))
            foreach (var property in context.Identity.Claims)
            {
                context.AdditionalResponseParameters.Add(property.Type, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            return base.TokenEndpointResponse(context);
        }
    }
}
