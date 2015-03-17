﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using ContactBook.WebApi.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;


namespace ContactBook.WebApi
{
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";

            UserManagerFactory = () => new UserManager<IdentityUser>(new UserStore<IdentityUser>());

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<IdentityUser>> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {

            //app.UseCors(CorsOptions.AllowAll);
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
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}