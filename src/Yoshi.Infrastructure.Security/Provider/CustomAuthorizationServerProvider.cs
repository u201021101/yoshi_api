using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Identity;
using Yoshi.EntityLayer.Model.Identity;
using Yoshi.EntityLayer.Service.Identity.Application;
using Yoshi.Infrastructure.OAuth.Model;

namespace Yoshi.Infrastructure.Security.Provider
{
    public class CustomAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        #region Services ---------------------
        private readonly IApplicationEntityService _applicationEntityService;
        #endregion
        #region Constructor ------------------
        public CustomAuthorizationServerProvider(IApplicationEntityService applicationEntityService)
        {
            this._applicationEntityService = applicationEntityService;
        }
        #endregion
        #region Override ---------------------        
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId, clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (string.IsNullOrEmpty(clientId))
            {
                context.SetError("invalid_client", "Missing client credentials.");
                context.Rejected();
            }
            else
            {
                ClientApplication client;
                var grant_type = context.Parameters.Get(OAuthNames.GrantType);

                if (string.Equals(grant_type, "password", StringComparison.OrdinalIgnoreCase))
                {
                    client = this._applicationEntityService.FindApplication(clientId);
                }
                else
                {
                    client = this._applicationEntityService.FindApplication(clientId, clientSecret);
                }

                if (client != null)
                {
                    context.OwinContext.Set<Guid>(OAuthNames.UserId, client.UserId);
                    context.Validated();
                }
                else
                {
                    context.SetError("invalid_client", "Client credentials are invalid.");
                    context.Rejected();
                }
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var signInManager = context.OwinContext.GetUserManager<ApplicationSignInManager>();

            var userId = context.OwinContext.Get<Guid>(OAuthNames.UserId);
            var user = userManager.FindByIdAsync(userId).Result;

            var identity = signInManager.CreateUserIdentity(user);

            var authProps = new AuthenticationProperties();

            authProps.Dictionary.Add(OAuthNames.ClientId, context.ClientId);
            authProps.ExpiresUtc = DateTimeOffset.Now.AddMinutes(120);

            var ticket = new AuthenticationTicket(identity, authProps);
            context.Validated(ticket);

            return base.GrantClientCredentials(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var signInManager = context.OwinContext.GetUserManager<ApplicationSignInManager>();
            var status = signInManager.PasswordSignIn(context.UserName, context.Password, true, false);


            context.Validated();
            return base.GrantResourceOwnerCredentials(context);
        }
        #endregion
    }
}
