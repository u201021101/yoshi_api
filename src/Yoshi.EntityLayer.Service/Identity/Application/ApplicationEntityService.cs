using System.Linq;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Model.Identity;

namespace Yoshi.EntityLayer.Service.Identity.Application
{
    public class ApplicationEntityService : EntityService<ClientApplication>, IApplicationEntityService
    {
        public ClientApplication FindApplication(string clientId)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Applications.FirstOrDefault<ClientApplication>(a => a.ClientId == clientId);

                return entity;
            }
        }

        public ClientApplication FindApplication(string clientId, string clientSecret)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = context.Applications.FirstOrDefault<ClientApplication>(a => a.ClientId == clientId && a.SecretKey == clientSecret);

                return entity;
            }
        }
    }
}
