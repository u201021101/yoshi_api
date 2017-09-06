using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.Infrastructure.Rest.Query;

namespace Yoshi.Infrastructure.Rest.Base
{
    public class BaseApiController : ApiController
    {
        protected Guid Identity
        {
            get
            {
                var user = (ClaimsIdentity)User.Identity;
                var identifier = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                return Guid.Parse(identifier.Value);
            }
        }

        protected QueryFilterOptions BuildQueryFilterOptions(ODataQuery options)
        {
            var queryOptions = new QueryFilterOptions() { PagingOptions = options.PagingOptions };

            return queryOptions;
        }
    }
}
