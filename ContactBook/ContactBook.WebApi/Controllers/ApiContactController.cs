using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts;
using ContactBook.Domain.Contexts.Contacts;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.Domain.Validations;
using ContactBook.WebApi.Filters;
using System;
using ContactBook.WebApi.Controllers.Helpers;

namespace ContactBook.WebApi.Controllers
{
    [RoutePrefix("api/ApiContact")]
    [Authorize]
    public class ApiContactController : ApiController
    {
        IContactBookRepositoryUow unitOfWork;
        IContactBookRepositoryUow readOnlyUow;
        
        public ApiContactController()
            : this(DependencyFactory.Resolve<IContactBookRepositoryUow>(), DependencyFactory.Resolve<IContactBookRepositoryUow>())
        {

        }

        public ApiContactController(IContactBookRepositoryUow pUnitOfWork, IContactBookRepositoryUow pReadOnlyUow)
        {
            unitOfWork = pUnitOfWork;
            readOnlyUow = pReadOnlyUow;
        }

        // GET api/<controller>
        [Route("{bookId}")]
        [ResponseType(typeof(List<Contact>))]
        [BookIdValidationFilter("bookId")]
        public IHttpActionResult Get(long bookId)
        {
            IContactContext  contactContext = new ContactContext(unitOfWork, readOnlyUow);
            List<Contact> contacts = contactContext.GetContacts(bookId);

            if (contacts == null && !contacts.Any())
            {
                return BadRequest();
            }

            return Ok<List<Contact>>(contacts);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Contact contact)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IContactContext contactContext = new ContactContext(unitOfWork, readOnlyUow);

            if (ApiHelper.TryExecuteContext(() => contactContext.InsertContact(contact), out exOut))
            {
                return Ok();
            }
            else
            {
                //todo: log the exception here
                return InternalServerError();
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]Contact contact)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            IContactContext contactContext = new ContactContext(unitOfWork, readOnlyUow);

            if (ApiHelper.TryExecuteContext(() => contactContext.UpdateContact(contact), out exOut))
            {
                return Ok();
            }
            else
            {
                //todo: log the exception here
                return InternalServerError();
            }
        }

        // DELETE api/<controller>/5
        [Route("{bookId}/{contactId}")]
        [BookIdValidationFilter("bookId")]
        public IHttpActionResult Delete(long bookId, long contactId)
        {
            Exception exOut;
            IContactContext contactContext = new ContactContext(unitOfWork, readOnlyUow);
            Contact contactToDel = contactContext.GetContact(bookId, contactId);
            
            if (contactToDel == null)
            {
                return NotFound();
            }

            if (ApiHelper.TryExecuteContext(() => contactContext.DeleteContact(contactToDel), out exOut))
            {
                return Ok();
            }
            else
            {
                //todo: log the exception here
                return InternalServerError();
            }
          
        }
    }
}