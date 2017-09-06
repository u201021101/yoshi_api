using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Yoshi.Rest.Startup))]

namespace Yoshi.Rest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Useful for Swagger implemetation
            GlobalConfiguration.Configure((configuration) => { configuration.MapHttpAttributeRoutes(); });

            HttpConfiguration config = new HttpConfiguration()
            {
                // Add this line to enable detail mode in release
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always 
            };

            DependencyConfig.Configure(config);
            WebApiConfig.Register(config);
            AuthConfig.Configure(app);
            MappingConfig.Register();

            app.UseWebApi(config);
        }
    }
}
