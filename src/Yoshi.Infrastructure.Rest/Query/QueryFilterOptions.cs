namespace Yoshi.Infrastructure.Rest.Query
{
    public class QueryFilterOptions
    {
        #region Properties -------------------
        public PagingOptions PagingOptions { get; set; }
        public OrderByOptions OrderByOptions { get; set; }
        #endregion
        #region Constructor ------------------
        public QueryFilterOptions()
        {
            this.PagingOptions = new PagingOptions();
            this.OrderByOptions = new OrderByOptions();
        }
        #endregion     
    }
}
