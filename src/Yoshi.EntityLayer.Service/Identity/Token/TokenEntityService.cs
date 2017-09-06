using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Model.Identity;

namespace Yoshi.EntityLayer.Service.Identity.Token
{
    public class TokenEntityService : EntityService<ClientToken>, ITokenEntityService
    {
        #region ITokenEntityService
        public string CreateAuthorizationCode(Guid userId)
        {
            var token = new ClientToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Code = Guid.NewGuid().ToString("n"),
                Type = TokenType.Code.ToString()
            };

            using (var context = new ApplicationDbContext())
            {
                context.Tokens.Add(token);
                context.SaveChanges();
            }

            return token.Code;
        }

        public ClientToken FindAuthorizationCode(string code)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Tokens.Where(c => c.Code == code && c.Type == TokenType.Code.ToString()).FirstOrDefault();

                return entity;
            }
        }

        public string CreateToken(Guid userId, Guid tokenId, string ticket)
        {
            var token = new ClientToken();
            token.UserId = userId;
            token.Id = tokenId;
            token.Code = string.Empty;
            token.Type = TokenType.Token.ToString();
            token.Ticket = ticket;

            using (var context = new ApplicationDbContext())
            {
                context.Tokens.Add(token);
                context.SaveChanges();
            }

            return token.Code;
        }
        #endregion
    }

    public enum TokenType
    {
        Code = 1,
        Token = 2
    }
}
