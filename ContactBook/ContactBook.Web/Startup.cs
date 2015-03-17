using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System.Web.Http;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;


namespace ContactBook.WebApi
{
    [assembly: OwinStartup(typeof(ContactBook.WebApi.Startup))]
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //activate authentication cookie
            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Accounts/Login"),
            //    LogoutPath = new PathString("/Accounts/Logout"),
            //    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
            //    SlidingExpiration = true
            //});
            ConfigureAuth(app);
        }
    }

    //public static class OAuthStartup
    //{
    //   static OAuthAuthorizationServerOptions OAuthOptions;
    //    static OAuthStartup()
    //    {
    //        OAuthOptions = new OAuthAuthorizationServerOptions()
    //        {
    //            TokenEndpointPath = "/Token",
    //            AuthorizeEndpointPath = "/api/Account/ExternalLogin"

    //        };
    //    }
    //}
}