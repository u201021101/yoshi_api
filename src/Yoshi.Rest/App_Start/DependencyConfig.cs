using Microsoft.Practices.Unity;
using System.Web.Http;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Service.Administration;
using Yoshi.Infrastructure.Dependency;
using Yoshi.QueryLayer.Administration.Location;
using Yoshi.QueryLayer.Administration.Merchant;
using Yoshi.TaskLayer.Administration;

namespace Yoshi.Rest
{
    public static class DependencyConfig
    {
        internal static void Configure(HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ApplicationDbContext>(new TransientLifetimeManager(), new InjectionConstructor("YoshiDb"));

            container.RegisterType<IMerchantEntityService, MerchantEntityService>();
            container.RegisterType<IMerchantQueryService, MerchantQueryService>();
            container.RegisterType<IMerchantService, MerchantService>();

            container.RegisterType<ILocationEntityService, LocationEntityService>();
            container.RegisterType<ILocationQueryService,  LocationQueryService>();
            container.RegisterType<ILocationService, LocationService>();

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}