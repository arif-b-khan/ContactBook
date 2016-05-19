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
using ContactBook.WebApi.App_Start;
using Microsoft.Practices.Unity;

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
            var container = UnityConfig.GetConfiguredContainer(); //get the configured container.
            var appDbContext = context.Get<CBIndentityDbContext>();
            var userStore = new UserStore<IdentityUser>(appDbContext);
            var appUserManager = new UserManager<IdentityUser>(userStore);

            container.RegisterInstance(userStore);
            container.RegisterInstance(appUserManager);
            container.RegisterInstance(Startup.OAuthOptions.AccessTokenFormat);
            
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

            //#if Azure
            DataProvider = options.DataProtectionProvider;
            //#else
            //            DataProvider = new DpapiDataProtectionProvider();
            //#endif

            if (DataProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(DataProvider.Create("UserToken"));
            }

            return appUserManager;
        }
    }
}