using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Service.Identity.Token;
using Yoshi.Infrastructure.OAuth.Model;

namespace Yoshi.Infrastructure.Security.Provider
{
    public class CustomRefreshTokenProvider : AuthenticationTokenProvider
    {
        #region Services ---------------------
        private readonly ITokenEntityService _tokenEntityService;
        #endregion
        #region Constructor ------------------
        public CustomRefreshTokenProvider(ITokenEntityService tokenEntityService)
        {
            this._tokenEntityService = tokenEntityService;
        }
        #endregion
        #region Override       
        public override Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var userId = context.OwinContext.Get<Guid>(OAuthNames.UserId);

            var tokenId = Guid.NewGuid();
            var refreshTokenId = tokenId;

            var protectedTicket = context.SerializeTicket();
            this._tokenEntityService.CreateToken(userId, refreshTokenId, protectedTicket);

            context.SetToken(refreshTokenId.ToString("n"));

            return Task.FromResult(0);
        }

        public override Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return base.ReceiveAsync(context);
        }
        #endregion
    }
}
