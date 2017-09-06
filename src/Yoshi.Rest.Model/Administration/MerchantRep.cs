using System;

namespace Yoshi.Rest.Model.Administration
{
    public abstract class BaseMerchantRep
    {
        public string Name { get; set; }
    }

    public class MerchantRep : BaseMerchantRep
    {
        public Guid Id { get; set; }
    }

    public class MerchantListRep : BaseMerchantRep
    {
        public Guid Id { get; set; }
    }

    public class MerchantPostRep : BaseMerchantRep
    {
    }
}
