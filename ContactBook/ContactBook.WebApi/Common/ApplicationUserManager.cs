using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactBook.WebApi.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace ContactBook.WebApi.Common
{
    public class ApplicationUserManager : UserManager<IdentityUser>
    {
        public static IDataProtectionProvider DataProvider;

        public ApplicationUserManager(IUserStore<IdentityUser> store)
            : base(store)
        {
        }

        public static UserManager<IdentityUser> Create(IdentityFactoryOptions<UserManager<IdentityUser>> options, IOwinContext context)
        {
            var appDbContext = context.Get<CBIndentityDbContext>();
            var appUserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(appDbContext));

            // Configure validation logic for usernames
            appUserManager.UserValidator = new UserValidator<IdentityUser>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 7,
                RequireDigit = false
            };

            appUserManager.EmailService = new ContactbookEmailService();

#if Azure
            DataProvider = options.DataProtectionProvider;
#else
            DataProvider = new DpapiDataProtectionProvider();
#endif

            if (DataProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(DataProvider.Create("UserToken"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return appUserManager;
        }
    }
}