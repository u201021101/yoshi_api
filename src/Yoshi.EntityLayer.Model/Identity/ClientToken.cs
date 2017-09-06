using System;

namespace Yoshi.EntityLayer.Model.Identity
{
    public class ClientToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Ticket { get; set; }
    }
}
