using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ContactBook.WebApi.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/ApiRole")]
    public class ApiRoleController : ApiController
    {
        public UserManager<IdentityUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ApiRoleController(): this(Startup.UserManagerFactory(), Startup.RoleManagerFactory())
        {

        }

        public ApiRoleController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        [HttpGet]
        [Route("Roles")]
        public List<IdentityRole> Get()
        {
            List<IdentityRole> roles = RoleManager.Roles.Select(e => e).ToList();
            return roles;
        }

    }
}