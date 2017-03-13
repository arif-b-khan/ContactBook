using System.Linq;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts;
using ContactBook.Domain.Models;
using ContactBook.WebApi.Providers;
using ContactBook.WebApi.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Transactions;
using ContactBook.Domain.Common.Logging;
using System.Web.Http.Tracing;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Contexts.Token;

namespace ContactBook.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class ApiAccountController : ApiController
    {
        private const string Category = "ApiAccountController";
        private const string LocalLoginProvider = "Local";
 
        private readonly ICBLogger _logger;

        private UserManager<IdentityUser> UserManager { get; set; }

        private ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }

        public ApiAccountController(UserManager<IdentityUser> userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat, ICBLogger logger)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
            _logger = logger;
        }

        //Get api/Account/UserExists
        [AllowAnonymous]
        [Route("UserExists")]
        public async Task<IHttpActionResult> GetUserExists(string username)
        {
            IdentityUser user = await UserManager.FindByNameAsync(username);
            if (user != null)
            {
                Configuration.Services.GetTraceWriter().Info(Request, Category, "Username {0} found in the database", username);
                return NotFound();
            }
            else
            {
                Configuration.Services.GetTraceWriter().Info(Request, Category, "Username {0} not found", username);
                return Ok();
            }
        }

        //Get api/Account/EmailExists
        [AllowAnonymous]
        [Route("EmailExists")]
        public async Task<IHttpActionResult> GetEmailExists(string email)
        {
            IdentityUser user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                Configuration.Services.GetTraceWriter().Info(Request, Category, "Email doesn't Exists:{0}", email);
                return Ok();
            }
            else
            {
                Configuration.Services.GetTraceWriter().Info(Request, Category, "Email Exists:{0}", email);
                return NotFound();
            }
        }

        [AllowAnonymous]
        [Route("ForgotEmailExists")]
        public async Task<IHttpActionResult> GetForgotEmailExists(string email)
        {
            IdentityUser user = await UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                Configuration.Services.GetTraceWriter().Info(Request, Category, "Email doesn't Exists:{0}", email);
                return Ok();
            }
            else
            {
                Configuration.Services.GetTraceWriter().Info(Request, Category, "Email Exists:{0}", email);
                return NotFound();
            }
        }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                UserName = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            Configuration.Services.GetTraceWriter().Info(Request, Category, "Username:{0}, Loging out", User.Identity.Name);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                Configuration.Services.GetTraceWriter().Warn(Request, Category, "ManageInfo: User not found");
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                UserName = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                Configuration.Services.GetTraceWriter().Warn(Request, Category, "ChangePassword: ModelError {0}", string.Join("; ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage)));
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityUser user = await UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound();
            }

            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

            try
            {
                Guid retGuid;
                bool savedToken = SaveGeneratedToken(user, code, ContactBookToken.PasswordToken, out retGuid);
                if (savedToken)
                {
                    string link = string.Format(model.Link + "?identifier={0}", HttpUtility.UrlEncode(retGuid.ToString()));
                    Task mailTask = UserManager.SendEmailAsync(user.Id, "Reset password", link);
                    mailTask.Wait();
                }
            }
            catch (AggregateException ex)
            {
                ex.Handle(e =>
                {
                    Configuration.Services.GetTraceWriter().Info(Request, Category, "Send mail failure " + e.Message);
                    return true;
                });
            }
            catch (Exception ex)
            {
                Configuration.Services.GetTraceWriter().Info(Request, Category, "ForgotPassword: Saved token failed: " + ex.Message);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contactToken = new ContactBookToken();
            Token userToken = contactToken.GetToken(model.Identifier);

            if (userToken != null)
            {

                IdentityResult result = await UserManager.ResetPasswordAsync(userToken.UserId, userToken.Token1, model.NewPassword);
                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }
                contactToken.DeleteToken(userToken.UserId);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            IdentityUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity oAuthIdentity = await UserManager.CreateIdentityAsync(user,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await UserManager.CreateIdentityAsync(user,
                    CookieAuthenticationDefaults.AuthenticationType);
                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public IHttpActionResult Register(RegisterBindingModel model)
        {
            bool registerSuccess = true;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            IdentityResult result;

            IHttpActionResult errorResult = null;

            using (TransactionScope tranScope = new TransactionScope())
            {
                result = UserManager.Create(user, model.Password);
                errorResult = GetErrorResult(result);

                if (errorResult == null)
                {

                    using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
                    {
                        IContactBookContext context = new ContactBookContext(uow);
                        context.CreateContactBook(model.UserName, user.Id);
                        uow.Save();
                    }
                    Configuration.Services.GetTraceWriter().Info(Request, Category, "User registered: Username: {0}, ContactBook: {1}", user.UserName, model.UserName + user.Id);
                    registerSuccess = true;
                }
                else
                {
                    Configuration.Services.GetTraceWriter().Info(Request, Category, "User registration failed.");

                }

                tranScope.Complete();
            }

            if (registerSuccess)
            {
                string code = string.Empty;
                try
                {

                    string userIdentityId = user.Id;
                    code = UserManager.GenerateEmailConfirmationToken(userIdentityId);
                    Guid retGuid;
                    bool saveToken = SaveGeneratedToken(user, code, ContactBookToken.EmailToken, out retGuid);

                    if (saveToken)
                    {
                        _logger.Info("Generate confiruation token: " + code);

                        string link = model.ConfirmUrl + string.Format("?identifier={0}", HttpUtility.UrlEncode(retGuid.ToString()));
                        Configuration.Services.GetTraceWriter().Info(Request, Category, "Account GenereatedLink: " + link);

                        UserManager.SendEmail(userIdentityId, "Contactbook confirmation", link);

                        Configuration.Services.GetTraceWriter().Info(Request, Category, "Email sent to user on this email address: " + model.Email);
                    }
                }
                catch (Exception ex)
                {
                    Configuration.Services.GetTraceWriter().Error(Request, Category, ex);
                    _logger.Error("Unable to send email Message", ex);
                }

                return CreatedAtRoute<RegisterBindingModel>("DefaultApi", new { Controller = "Account", Action = "ConfirmEmail", userId = user.Id, code = code }, model);
            }
            else
            {
                return errorResult;
            }
        }

        private static bool SaveGeneratedToken(IdentityUser user, string code, string tokenType, out Guid guid)
        {
            IContactBookToken tokenDb = new ContactBookToken();
            guid = Guid.NewGuid();
            bool saveToken = tokenDb.SaveToken(user.Id, code, tokenType, guid);
            return saveToken;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail")]
        public IHttpActionResult GetConfirmEmail(string identifier)
        {

            var contactToken = new ContactBookToken();
            Token cbToken = contactToken.GetToken(identifier);

            IdentityResult idResult = null;

            if (cbToken != null && !string.IsNullOrEmpty(cbToken.UserId))
            {
                idResult = UserManager.ConfirmEmail(cbToken.UserId, cbToken.Token1);
            }
            else
            {
                return InternalServerError(new Exception("Unable to find token information"));
            }

            IHttpActionResult result = GetErrorResult(idResult);

            if (result == null)
            {
                contactToken.DeleteToken(cbToken.UserId);
                return Ok();
            }
            else
            {
                return result;
            }
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            IdentityUser user = new IdentityUser
            {
                UserName = model.UserName
            };
            user.Logins.Add(new IdentityUserLogin
            {
                LoginProvider = externalLogin.LoginProvider,
                ProviderKey = externalLogin.ProviderKey
            });
            IdentityResult result = await UserManager.CreateAsync(user);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                        ITraceWriter writer = Configuration.Services.GetTraceWriter();
                        writer.Error(Request, Category, "MethodName: GetErrorResult Message: {0}", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }

            public string ProviderKey { get; set; }

            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }
    }

}