using System.Data.Entity;
using Yoshi.EntityLayer.Model.Administration;
using Yoshi.EntityLayer.Model.Identity;

namespace Yoshi.EntityLayer.Context
{
    public interface IApplicationDbContext
    {
        IDbSet<ClientApplication> Applications { get; set; }
        IDbSet<ClientToken> Tokens { get; set; }
        IDbSet<Merchant> Merchants { get; set; }
    }
}
