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
using System.Threading.Tasks;

namespace ContactBook.WebApi.Controllers
{
    [RoutePrefix("api/ApiAddressType")]
    [Authorize]
    public class ApiAddressTypeController : ApiController
    {
        private IContactBookRepositoryUow unitOfWork;
        private IGenericContextTypes<AddressType, CB_AddressType> genericContext;
        private IGenericContextTypes<AddressType, CB_AddressType> readOnlyContext;
        private IContactBookRepositoryUow getUnitOfWork;
        
        public ApiAddressTypeController(IContactBookRepositoryUow uow, IContactBookRepositoryUow getUnitOfWork)
        {
            unitOfWork = uow;
            this.getUnitOfWork = getUnitOfWork;
            genericContext = new GenericContextTypes<AddressType, CB_AddressType>(unitOfWork);
            readOnlyContext = new GenericContextTypes<AddressType, CB_AddressType>(getUnitOfWork);
        }

        // GET api/AddressType/GetTypes/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<AddressType>))]
        [BookIdValidationFilter("bookId")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(long bookId)
        {
            List<AddressType> retAddressType = genericContext.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue));

            if (retAddressType == null || retAddressType.Count == 0)
            {
                return NotFound();
            }

            return Ok(retAddressType);
        }

        //Post api/AddressType/InsertType
        [HttpPost]
        public async IHttpActionResult Post([FromBody]AddressType addressType)
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
                return CreatedAtRoute<AddressType>("DefaultApi", new { controller = "AddressType", action = "GetTypes", bookid = addressType.BookId }, addressType);
            }
            else
            {
                return InternalServerError(retException);
            }
        }

        // PUT api/<controller>/5
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
                List<AddressType> existingTypeList = readOnlyContext.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) && cbt.AddressTypeId == addressType.AddressTypeId));

                if (existingTypeList == null || existingTypeList.Count == 0)
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

        //    // DELETE api/DeleteType/5
        [Route("{addressTypeId}/{bookId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int addressTypeId, long bookId)
        {
            AddressType addressType = null;

            if (bookId <= 0)
            {
                return BadRequest(string.Format("Invalid book id {0}", bookId));
            }

            addressType = readOnlyContext.GetTypes(cb => cb.AddressTypeId == addressTypeId && (cb.BookId.HasValue && cb.BookId.Value == bookId)).SingleOrDefault();

            if (addressType == null)
            {
                return NotFound();
            }

            if (addressType.BookId.HasValue)
            {
                genericContext.DeleteTypes(new List<AddressType>() { addressType });
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default Address type.");
            }

            return Ok();
        }
    }
}