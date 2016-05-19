using Owin;
using System.Diagnostics;

using System.Web.Http;

namespace ContactBook.WebApi
{
    //[assembly: OwinStartup(typeof(ContactBook.WebApi.Startup))]

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
            //ContactBookGlobalConfiguration.ApplicationStart();
            ConfigureAuth(app);
            Debug.WriteLine("Outside selfthost tag");

#if USESELFHOST
            var config = new HttpConfiguration();
            Debug.WriteLine("Registering configuration");
            WebApiConfig.Register(config);
            app.UseWebApi(config);
#endif
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