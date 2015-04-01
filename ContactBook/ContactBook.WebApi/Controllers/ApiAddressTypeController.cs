using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContactBook.Db.Repositories;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.Domain.Contexts.Generics;
using ContactBook.Db.Data;
using System.Linq.Expressions;

namespace ContactBook.WebApi.Controllers
{
    [RoutePrefix("api/AddressType")]
    [Authorize]
    public class ApiAddressTypeController : ApiController
    {
        IContactBookRepositoryUow unitOfWork;
        IGenericContextTypes<AddressType, CB_AddressType> genericContext;
        public ApiAddressTypeController()
            : this(DependencyFactory.Resolve<IContactBookRepositoryUow>())
        {

        }

        public ApiAddressTypeController(IContactBookRepositoryUow uow)
        {
            unitOfWork = uow;
            genericContext = new GenericContextTypes<AddressType, CB_AddressType>(unitOfWork);
        }

        // GET api/<controller>
        [Route("GetTypes/{bookId}")]
        [ResponseType(typeof(List<AddressType>))]
        [HttpGet]
        public IHttpActionResult Get(long bookId)
        {

            List<AddressType> retAddressType = genericContext.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue));

            if (retAddressType == null)
            {
                return NotFound();
            }

            return Ok<List<AddressType>>(retAddressType);
        }

        [Route("InsertTypes")]
        [HttpPost]
        public IHttpActionResult InsertAddressTypes([FromBody]List<AddressType> addressTypes)
        {
            Exception retException = null;
            bool status = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                genericContext.InsertTypes(addressTypes);
            }
            catch (Exception ex)
            {
                //todo: log this exception
                status = false;
                retException = ex;
            }

            if (status)
            {
                var routeValue = new Dictionary<string, object>();
                routeValue.Add("bookId", "1");
                return CreatedAtRoute<List<AddressType>>("GetTypes", routeValue , addressTypes);
            }
            else
            {
                return InternalServerError(retException);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}