using Microsoft.AspNet.Identity.EntityFramework;

namespace ContactBook.WebApi.Context
{
    public class CBIndentityDbContext : IdentityDbContext<IdentityUser>
    {
        public CBIndentityDbContext()
            : base("name=DefaultConnection")
        {
        }
    }
}