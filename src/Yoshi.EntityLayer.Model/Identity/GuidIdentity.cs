using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Yoshi.EntityLayer.Model.Identity
{
    public class GuidIdentityUserLogin : IdentityUserLogin<Guid> { }
    public class GuidIdentityUserRole : IdentityUserRole<Guid> { }
    public class GuidIdentityUserClaim : IdentityUserClaim<Guid> { }
    public class GuidIdentityUser : IdentityUser<Guid, GuidIdentityUserLogin, GuidIdentityUserRole, GuidIdentityUserClaim>
    {
        public string Name { get; set; }

        #region Audit ------------------------
        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<GuidIdentityUser, Guid> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
    public class GuidIdentityRole : IdentityRole<Guid, GuidIdentityUserRole>
    {

    }
    public class GuidUserStore : UserStore<GuidIdentityUser, GuidIdentityRole, Guid, GuidIdentityUserLogin, GuidIdentityUserRole, GuidIdentityUserClaim>
    {
        public GuidUserStore(DbContext context) : base(context) { }
    }
}
