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
        private IContactBookRepositoryUow unitOfWork = null;

        public ApiContactBookController()
            : this(DependencyFactory.Resolve<IContactBookRepositoryUow>())
        {
        }

        public ApiContactBookController(IContactBookRepositoryUow unitOfWork)
        {
            if (unitOfWork != null)
            {
                this.unitOfWork = unitOfWork;
                unitOfWork.GetEntityByType<CB_ContactBook>();
            }
        }

        //todo: remove the username parameter from getcontactbook it is a security issuse
        [Authorize]
        [Route("GetContactBook/{username}")]
        public IHttpActionResult GetContactBook(string username)
        {
            ContactBookInfo cbInfo = null;
            IContactBookContext cbContext = new ContactBookContext(unitOfWork);
            cbInfo = cbContext.GetContactBook(username);

            if (cbInfo == null)
            {
                return NotFound();
            }

            return Ok(cbInfo);
        }
    }
}