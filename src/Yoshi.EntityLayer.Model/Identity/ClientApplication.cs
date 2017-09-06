using System;
using System.ComponentModel.DataAnnotations;

namespace Yoshi.EntityLayer.Model.Identity
{
    public class ClientApplication
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [StringLength(64)]
        public string ClientId { get; set; }

        [StringLength(64)]
        public string SecretKey { get; set; }
    }
}
