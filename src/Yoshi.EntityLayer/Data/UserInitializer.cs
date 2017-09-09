using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Model.Identity;
using Yoshi.EntityLayer.Model.Names;

namespace Yoshi.EntityLayer.Data
{
    internal class UserInitializer
    {
        public static void Register(ApplicationDbContext context)
        {

            var userManager = new UserManager<GuidIdentityUser, Guid>(new GuidUserStore(context));

            if (!context.Users.Any(u => u.Id == UserNames.SystemId))
            {
                var user = new GuidIdentityUser
                {
                    Id = UserNames.SystemId,
                    UserName = UserNames.System,
                    CreatedBy = UserNames.SystemId,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, "1234567#");
                userManager.AddToRole(user.Id, RoleNames.Administrator);
                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.Id == UserNames.AppMerchantId))
            {
                var user = new GuidIdentityUser
                {
                    Id = UserNames.AppMerchantId,
                    UserName = UserNames.AppMerchant,
                    CreatedBy = UserNames.SystemId,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, "1234567#");
                userManager.AddToRole(user.Id, RoleNames.Administrator);
                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.Id == UserNames.AppCustomerId))
            {
                var user = new GuidIdentityUser
                {
                    Id = UserNames.AppCustomerId,
                    UserName = UserNames.AppCustomer,
                    CreatedBy = UserNames.SystemId,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, "1234567#");
                userManager.AddToRole(user.Id, RoleNames.Administrator);
                context.SaveChanges();
            }
        }
    }
}
