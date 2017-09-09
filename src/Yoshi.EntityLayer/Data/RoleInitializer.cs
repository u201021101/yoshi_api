using System.Linq;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Model.Identity;
using Yoshi.EntityLayer.Model.Names;

namespace Yoshi.EntityLayer.Data
{
    internal class RoleInitializer
    {
        public static void Register(ApplicationDbContext context)
        {
            if (!context.Roles.Any(c => c.Id == RoleNames.AdministratorId))
            {
                context.Roles.Add(new GuidIdentityRole { Id = RoleNames.AdministratorId, Name = RoleNames.Administrator });
            }
          
            context.SaveChanges();

        }
    }
}
