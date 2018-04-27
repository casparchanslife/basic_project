using Learn.Lib;
using Microsoft.Owin.Security.Infrastructure;
using System;

namespace Learn.WebAPIService.Manager
{
    public class RefreshTokenManager : AuthenticationTokenProvider
    {

        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddMinutes(OAuthGlobal.OAuth_RefreshToken_Expire_Minute));
            context.SetToken(context.SerializeTicket());

        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
            /*
            var AccessTokenRepository = new AccessTokenRepository();
            bool exist = AccessTokenRepository.removeRefreshToken(context.Token);
            bool valid = false;
            if (exist)
            {
                context.DeserializeTicket(context.Token);
                if (context.Ticket != null)
                {
                    valid = true;
                }
            }
            LoggingUtil.GetLogger().Statistic(String.Format("GrantType=refresh_token Valid={0} Exist={1} Token={2}", valid, exist, context.Token));
            */
        }
    }
}
