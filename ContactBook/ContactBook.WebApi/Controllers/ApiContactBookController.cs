using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using System.Web.Http;

namespace ContactBook.WebApi.Controllers
{
    [RoutePrefix("api/ApiContactBook")]
    public class ApiContactBookController : ApiController
    {
        IContactBookContext contactContext;

        public ApiContactBookController(IContactBookContext pcontactContext)
        {
            contactContext = pcontactContext;
        }

        //todo: remove the username parameter from getcontactbook it is a security issuse
        [Authorize]
        [Route("GetContactBook/{username}")]
        public IHttpActionResult GetContactBook(string username)
        {
            ContactBookInfo cbInfo = null;

            cbInfo = contactContext.GetContactBook(username);

            if (cbInfo == null)
            {
                return NotFound();
            }

            return Ok(cbInfo);
        }
    }
}