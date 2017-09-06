using System;
using Yoshi.EntityLayer.Model.Identity;

namespace Yoshi.EntityLayer.Service.Identity.Token
{
    public interface ITokenEntityService : IEntityService<ClientToken>
    {
        string CreateAuthorizationCode(Guid userId);
        ClientToken FindAuthorizationCode(string code);
        string CreateToken(Guid userId, Guid tokenId, string ticket);
    }
}
