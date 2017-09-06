using Yoshi.Infrastructure.Rest.Query;
using System.Web.Http.ModelBinding;

namespace Yoshi.Infrastructure.Rest.OData
{
    [ModelBinder(typeof(ODataQueryModelBinder))]
    public class ODataQuery
    {
        #region Properties -------------------
        public Filter[] Filters { get; set; }
        public PagingOptions PagingOptions { get; set; }
        public OrderByOptions[] OrderByOptions { get; set; }
        #endregion
        #region Constructor ------------------
        public ODataQuery()
        {
            this.Filters = new Filter[] { };
            this.PagingOptions = new PagingOptions();
        }
        #endregion
    }
}
