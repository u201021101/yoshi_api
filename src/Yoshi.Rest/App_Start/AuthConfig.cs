using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Identity;
using Yoshi.EntityLayer.Service.Identity.Application;
using Yoshi.EntityLayer.Service.Identity.Token;
using Yoshi.Infrastructure.Security.Provider;

namespace Yoshi.Rest
{
    public class AuthConfig
    {
        public static void Configure(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            OAuthAuthorizationServerOptions authorization = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                //AuthorizeEndpointPath = new PathString("/oauth/authorize"),
                Provider = new CustomAuthorizationServerProvider(new ApplicationEntityService()),
                AuthorizationCodeProvider = new CustomAuthorizationCodeProvider(new TokenEntityService()),
                RefreshTokenProvider = new CustomRefreshTokenProvider(new TokenEntityService()),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(120),
                AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(15)
            };
            app.UseOAuthAuthorizationServer(authorization);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}