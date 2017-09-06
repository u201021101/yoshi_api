namespace Yoshi.Infrastructure.Rest.OData
{
    public class Filter
    {
        public string Attribute { get; set; }
        public FilterOperator Operator { get; set; }
        public string Value { get; set; }
    }

    public enum FilterOperator
    {
        lk = 0,
        eq = 1,
        ne = 2,
        gt = 3,
        lt = 4
    }
}
