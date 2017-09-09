using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.Infrastructure.Rest.PagedList;
using Yoshi.Infrastructure.Rest.Query;
using Yoshi.QueryLayer.Model.Administration;

namespace Yoshi.QueryLayer.Administration.Location
{
    public interface ILocationQueryService
    {
        LocationDto FindById(Guid id);
        IEnumerable<LocationDto> Find();
        IPagedList<LocationDto> Search(Filter[] filters, QueryFilterOptions queryOptions = null);
    }
}
