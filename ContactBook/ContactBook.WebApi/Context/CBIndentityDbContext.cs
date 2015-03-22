using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactBook.WebApi.Context
{
    public class CBIndentityDbContext : IdentityDbContext<IdentityUser>
    {
        public CBIndentityDbContext(): base("name=DefaultConnection")
        {

        }
    }
}