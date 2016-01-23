using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;

namespace ContactBook.WebApi.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/ApiRole")]
    public class ApiRoleController : ApiController
    {
        public UserManager<IdentityUser> UserManager { get; private set; }

        public ApiRoleController(): this(HttpContext.Current.GetOwinContext().GetUserManager<UserManager<IdentityUser>>())
        {
            
        }

        public ApiRoleController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;

        }

        //[HttpGet]
        //[Route("Roles")]
        //public List<IdentityRole> Get()
        //{
        //    //List<IdentityRole> roles = RoleManager.Roles.Select(e => e).ToList();
        //    return roles;
        //}

    }
}