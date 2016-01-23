using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Generics;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.WebApi.Controllers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ContactBook.WebApi.Filters;

namespace ContactBook.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/ApiNumberType")]
    public class ApiNumberTypeController : ApiController
    {
        private IContactBookRepositoryUow _unitofWork;
        private IContactBookRepositoryUow _readOnlyUow;
        private IGenericContextTypes<NumberType, CB_NumberType> numberTypeRepo;
        private IGenericContextTypes<NumberType, CB_NumberType> readOnlyRepo;
        
        public ApiNumberTypeController(IContactBookRepositoryUow unitofWork, IContactBookRepositoryUow readOnlyUow)
        {
            _unitofWork = unitofWork;
            _readOnlyUow = readOnlyUow;
            numberTypeRepo = new GenericContextTypes<NumberType, CB_NumberType>(unitofWork);
            readOnlyRepo = new GenericContextTypes<NumberType, CB_NumberType>(_readOnlyUow);
        }

        //Get api/<controller>/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<NumberType>))]
        [BookIdValidationFilter("bookId")]
        public IHttpActionResult Get(long bookId)
        {
            List<NumberType> numberTypes = numberTypeRepo.GetTypes(nbt => ((nbt.BookId.HasValue && nbt.BookId.Value == bookId) || !nbt.BookId.HasValue));
            if (numberTypes == null || numberTypes.Count == 0)
            {
                return NotFound();
            }
            return Ok(numberTypes);
        }

        // POST api/<controller>

        public IHttpActionResult Post([FromBody]NumberType numberType)
        {
            Exception exOut;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ApiHelper.TryExecuteContext(() => numberTypeRepo.InsertTypes(new List<NumberType>() { numberType }), out exOut))
            {
                return CreatedAtRoute<NumberType>("DefaultApi", new { controller = "ApiNumberType", action = "Get", bookId = numberType.BookId }, numberType);
            }
            else
            {
                return InternalServerError(exOut);
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]NumberType pNumberType)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<NumberType> numberTypeList = readOnlyRepo.GetTypes(nbt => nbt.NumberTypeId == pNumberType.NumberTypeId && (nbt.BookId.HasValue && nbt.BookId.Value == pNumberType.BookId));

            if (numberTypeList == null || numberTypeList.Count == 0)
            {
                return NotFound();
            }

            NumberType dbNumberType = numberTypeList.SingleOrDefault();

            if (dbNumberType != null && !dbNumberType.Equals(pNumberType))
            {
                if (ApiHelper.TryExecuteContext(() => numberTypeRepo.UpdateTypes(new List<NumberType>() { pNumberType }), out exOut))
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError(exOut);
                }
            }
            return NotFound();
        }

        // DELETE api/<controller>/5
        [Route("{bookId}/{typeId}")]
        public IHttpActionResult Delete(long bookId, int typeId)
        {
            NumberType numberType = null;

            if (bookId <= 0)
            {
                return BadRequest(string.Format("Invalid book id {0}", bookId));
            }

            numberType = readOnlyRepo.GetTypes(nb => nb.NumberTypeId == typeId && (nb.BookId.HasValue && nb.BookId.Value == bookId)).SingleOrDefault();

            if (numberType == null)
            {
                return NotFound();
            }

            if (numberType.BookId.HasValue)
            {
                numberTypeRepo.DeleteTypes(new List<NumberType>() { numberType });
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default Number type.");
            }

            return Ok();
        }
    }
}