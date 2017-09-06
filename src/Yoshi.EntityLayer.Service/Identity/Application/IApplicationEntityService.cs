using Yoshi.EntityLayer.Model.Identity;

namespace Yoshi.EntityLayer.Service.Identity.Application
{
    public interface IApplicationEntityService : IEntityService<ClientApplication>
    {
        ClientApplication FindApplication(string clientId);
        ClientApplication FindApplication(string clientId, string clientSecret);
    }
}
