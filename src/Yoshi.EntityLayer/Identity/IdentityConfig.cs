using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using Yoshi.EntityLayer.Context;
using Yoshi.EntityLayer.Model.Identity;

namespace Yoshi.EntityLayer.Identity
{
    public class ApplicationSignInManager : SignInManager<GuidIdentityUser, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) { }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class ApplicationUserManager : UserManager<GuidIdentityUser, Guid>
    {
        public ApplicationUserManager(IUserStore<GuidIdentityUser, Guid> store) : base(store) { }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new GuidUserStore(context.Get<ApplicationDbContext>()));

            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 8,
                RequireDigit = true
            };

            manager.UserLockoutEnabledByDefault = true;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<GuidIdentityUser, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

    }
}
