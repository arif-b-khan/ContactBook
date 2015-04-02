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

        // GET api/AddressType/GetTypes/1
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

        //Post api/AddressType/InsertType
        [Route("InsertType")]
        [HttpPost]
        public IHttpActionResult InsertAddressTypes([FromBody]AddressType addressType)
        {
            Exception retException = null;
            bool status = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                genericContext.InsertTypes(new List<AddressType>() { addressType });
            }
            catch (Exception ex)
            {
                //todo: log this exception
                status = false;
                retException = ex;
            }

            if (status)
            {
                return CreatedAtRoute<AddressType>("DefaultApi", new { controller = "AddressType", action = "GetTypes", bookid = 1 }, addressType);
            }
            else
            {
                return InternalServerError(retException);
            }
        }

        // PUT api/<controller>/5
        [Route("UpdateType")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]AddressType addressType)
        {
            Exception retException = null;
            bool status = true;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            long bookId = addressType.BookId.HasValue ? addressType.BookId.Value : 0;

            try
            {
                IGenericContextTypes<AddressType, CB_AddressType> existingType = new GenericContextTypes<AddressType, CB_AddressType>(unitOfWork);
                List<AddressType> existingTypeList = existingType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == addressType.BookId.Value) && cbt.AddressTypeId == addressType.AddressTypeId));

                if (existingTypeList == null)
                {
                    return NotFound();
                }

                AddressType existingAddressType = existingTypeList.SingleOrDefault();

                if (!existingAddressType.Equals(addressType))
                {
                    genericContext.UpdateTypes(new List<AddressType>() { addressType });
                }
            }
            catch (Exception ex)
            {
                //todo: log this exception
                status = false;
                retException = ex;
            }

            if (status)
            {
                return Ok();
            }
            else
            {
                return InternalServerError(retException);
            }
        }

        //    // DELETE api/<controller>/5
        //    public void Delete(int id)
        //    {
        //    }
    }
}