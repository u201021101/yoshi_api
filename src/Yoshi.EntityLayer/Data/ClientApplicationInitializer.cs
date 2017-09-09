using System;
using System.Linq;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Model.Identity;
using Yoshi.EntityLayer.Model.Names;

namespace Yoshi.EntityLayer.Data
{
    internal class ClientApplicationInitializer
    {
        public static void Register(ApplicationDbContext context)
        {
            if (!context.Applications.Any(c => c.ClientId == ClientApplicationNames.AppMerchantsId))
            {
                context.Applications.Add(new ClientApplication
                {
                    Id = Guid.NewGuid(),
                    ClientId = ClientApplicationNames.AppMerchantsId,
                    SecretKey = ClientApplicationNames.AppMerchantsSecret,
                    UserId = UserNames.AppMerchantId
                });
            }

            context.SaveChanges();
        }
    }
}
