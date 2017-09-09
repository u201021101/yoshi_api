using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using Yoshi.EntityLayer.Migrations;
using Yoshi.EntityLayer.Model.Administration;
using Yoshi.EntityLayer.Model.Identity;

namespace Yoshi.EntityLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<GuidIdentityUser, GuidIdentityRole, Guid, GuidIdentityUserLogin, GuidIdentityUserRole, GuidIdentityUserClaim>, IApplicationDbContext, IDisposable
    {
        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString: nameOrConnectionString)
        {
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public ApplicationDbContext() : this("YoshiDb") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #region IApplicationDbContext
        public IDbSet<ClientApplication> Applications { get; set; }
        public IDbSet<ClientToken> Tokens { get; set; }
        public IDbSet<Merchant> Merchants { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<SportField> SportsFields { get; set; }
        #endregion
    }
}
