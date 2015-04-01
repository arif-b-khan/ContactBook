using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ContactBook.WebApi.Controllers
{
    [RoutePrefix("api/ContactBook")]
    public class ApiContactBookController : ApiController
    {
        IContactBookRepositoryUow unitOfWork = null;

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