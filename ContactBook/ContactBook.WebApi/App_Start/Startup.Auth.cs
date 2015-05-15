using ContactBook.WebApi.Context;
using ContactBook.WebApi.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Cors;
using ContactBook.WebApi.Common;
using ContactBook.Domain.IoC;
using Microsoft.Owin.Security.DataProtection;
using System.Web;

namespace ContactBook.WebApi
{
    public partial class Startup
    {
        
        public static string PublicClientId = "Public";

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CBIndentityDbContext.Create);
            app.CreatePerOwinContext<UserManager<IdentityUser>>((IdentityFactoryOptions<UserManager<IdentityUser>> opt, IOwinContext con) =>
                {
                    UserManager<IdentityUser> retUserManager = ApplicationUserManager.Create(opt, con);
                    return retUserManager;
                });

           
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            //app.CreatePerOwinContext<UserManager>(
            app.UseCors(CorsOptions.AllowAll);
            // restrict policy to an end point if webapi cors is enabled...
            app.UseCors(new CorsOptions()
            {
                PolicyProvider = new CorsPolicyProvider()
                {
                    PolicyResolver = request =>
                    {
                        if (request.Path.StartsWithSegments(new PathString("/token")))
                        {
                            return Task.FromResult(new CorsPolicy { AllowAnyOrigin = true });
                        }
                        return Task.FromResult<CorsPolicy>(null);
                    }
                }
            });

            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationType = "Application",
            //    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
            //    LoginPath = new PathString("/default.html"),
            //    LogoutPath = new PathString("/account/Logout")
            //});
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            ConfigureOAuthTokenGeneration(app);
        }
    }
}