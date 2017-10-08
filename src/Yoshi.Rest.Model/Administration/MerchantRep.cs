using System;
using System.ComponentModel.DataAnnotations;

namespace Yoshi.Rest.Model.Administration
{
    public abstract class BaseMerchantRep
    {
        [Required]
        public Guid IdUser { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BusinessName { get; set; }

        [Required]
        public string TaxId { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        public string ContactPhone { get; set; }
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
