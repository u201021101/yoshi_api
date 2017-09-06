using System;
using System.Collections.Generic;
using Yoshi.Infrastructure.Rest.OData;
using Yoshi.Infrastructure.Rest.PagedList;
using Yoshi.Infrastructure.Rest.Query;
using Yoshi.QueryLayer.Model.Administration;

namespace Yoshi.QueryLayer.Administration.Merchant
{
    public interface IMerchantQueryService
    {
        MerchantDto FindById(Guid id);
        IEnumerable<MerchantDto> Find();
        IPagedList<MerchantDto> Search(Filter[] filters, QueryFilterOptions queryOptions = null);        
    }
}
