using System;
using System.Collections.Generic;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.Infrastructure.Rest.PagedList;
using Yoshi.Infrastructure.Rest.Query;
using Yoshi.QueryLayer.Model.Administration;

namespace Yoshi.QueryLayer.Administration.User
{
    public interface IUserQueryService
    {
        UserDto FindById(Guid id);
        IEnumerable<UserDto> Find();
        IPagedList<UserDto> Search(Filter[] filters, QueryFilterOptions queryOptions = null);        
    }
}
