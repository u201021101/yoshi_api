using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using Yoshi.EntityLayer.Identity;
using Yoshi.EntityLayer.Service.Identity.Token;
using Yoshi.Infrastructure.OAuth.Model;

namespace Yoshi.Infrastructure.Security.Provider
{
    public class CustomAuthorizationCodeProvider : AuthenticationTokenProvider
    {
        #region Services ---------------------
        private readonly ITokenEntityService _tokenEntityService;
        #endregion
        #region Constructor ------------------
        public CustomAuthorizationCodeProvider(ITokenEntityService tokenEntityService)
        {
            this._tokenEntityService = tokenEntityService;
        }
        #endregion
        #region AuthenticationTokenProvider
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            if (!string.IsNullOrEmpty(context.Token))
            {
                object form;
                var token = _tokenEntityService.FindAuthorizationCode(context.Token);

                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                var user = userManager.FindByIdAsync(token.UserId).Result;

                var signInManager = context.OwinContext.GetUserManager<ApplicationSignInManager>();
                var identity = signInManager.CreateUserIdentity(user);


                context.OwinContext.Environment.TryGetValue("Microsoft.Owin.Form#collection", out form);
                var clientIds = (form as FormCollection).GetValues(OAuthNames.ClientId);

                var authProps = new AuthenticationProperties();
                authProps.Dictionary.Add(OAuthNames.ClientId, clientIds[0]);
                authProps.ExpiresUtc = DateTimeOffset.Now.AddMinutes(120);

                var ticket = new AuthenticationTicket(identity, authProps);
                context.SetTicket(ticket);
            }
        }
        #endregion
    }
}
