using System;
using System.ComponentModel.DataAnnotations;

namespace Yoshi.Rest.Model.Administration
{
    public abstract class BaseUserRep
    {
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

    public class UserRep : BaseUserRep
    {
        public Guid Id { get; set; }
    }

    public class UserListRep : BaseUserRep
    {
        public Guid Id { get; set; }
    }

    public class UserPostRep : BaseUserRep
    {
    }
}
