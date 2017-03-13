using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Generics;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ContactBook.WebApi.Filters;

namespace ContactBook.WebApi.Controllers
{
    [RoutePrefix("api/ApiAddressType")]
    [Authorize]
    public class ApiAddressTypeController : ApiController
    {
        private IGenericContextTypes<AddressTypeModel, AddressType> genericContext;
        
        public ApiAddressTypeController(IContactBookRepositoryUow uow)
        {
            genericContext = new GenericContextTypes<AddressTypeModel, AddressType>(uow);
        }

        // GET api/AddressType/GetTypes/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<AddressTypeModel>))]
        [BookIdValidationFilter("bookId")]
        [HttpGet]
        public IHttpActionResult Get(long bookId)
        {
            List<AddressTypeModel> retAddressType = genericContext.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue));

            if (retAddressType == null || retAddressType.Count == 0)
            {
                return NotFound();
            }

            return Ok(retAddressType);
        }

        //Post api/AddressType/InsertType
        [HttpPost]
        public IHttpActionResult Post([FromBody]AddressTypeModel addressType)
        {
            Exception retException = null;
            bool status = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                genericContext.InsertTypes(new List<AddressTypeModel>() { addressType });
            }
            catch (Exception ex)
            {
                //todo: log this exception
                status = false;
                retException = ex;
            }

            if (status)
            {
                return CreatedAtRoute<AddressTypeModel>("DefaultApi", new { controller = "AddressType", action = "GetTypes", bookid = addressType.BookId }, addressType);
            }
            else
            {
                return InternalServerError(retException);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put([FromBody]AddressTypeModel addressType)
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
                List<AddressType> existingTypeList = genericContext.GetCBTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) && cbt.AddressTypeId == addressType.AddressTypeId));

                if (existingTypeList == null || existingTypeList.Count == 0)
                {
                    return NotFound();
                }

                AddressType existingAddressType = existingTypeList.SingleOrDefault();

                if (!existingAddressType.Equals(addressType))
                {
                    genericContext.UpdateTypes(existingAddressType, addressType);
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

        //    // DELETE api/DeleteType/5
        [Route("{addressTypeId}/{bookId}")]
        [BookIdValidationFilter("bookId")]
        [HttpDelete]
        public IHttpActionResult Delete(int addressTypeId, long bookId)
        {
            AddressType addressType = null;

            if (bookId <= 0)
            {
                return BadRequest(string.Format("Invalid book id {0}", bookId));
            }

            addressType = genericContext.GetCBTypes(cb => cb.AddressTypeId == addressTypeId && (cb.BookId.HasValue && cb.BookId.Value == bookId)).SingleOrDefault();

            if (addressType == null)
            {
                return NotFound();
            }

            if (addressType.BookId.HasValue)
            {
                genericContext.DeleteTypes(addressType);
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default Address type.");
            }

            return Ok();
        }
    }
}