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
    [RoutePrefix("api/ApiSpecialDateType")]
    public class ApiSpecialDateTypeController : ApiController
    {
        private IContactBookRepositoryUow _unitofWork;
        private IContactBookRepositoryUow _readOnlyUow;
        private IGenericContextTypes<SpecialDateTypeModel, SpecialDateType> specialDateTypeRepo;
        private IGenericContextTypes<SpecialDateTypeModel, SpecialDateType> readOnlyRepo;
        
        public ApiSpecialDateTypeController(IContactBookRepositoryUow unitofWork, IContactBookRepositoryUow readOnlyUow)
        {
            _unitofWork = unitofWork;
            _readOnlyUow = readOnlyUow;
            specialDateTypeRepo = new GenericContextTypes<SpecialDateTypeModel, SpecialDateType>(unitofWork);
            readOnlyRepo = new GenericContextTypes<SpecialDateTypeModel, SpecialDateType>(_readOnlyUow);
        }

        //Get api/SpecialDateType/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<SpecialDateTypeModel>))]
        [BookIdValidationFilter("bookId")]
        public IHttpActionResult Get(long bookId)
        {
            List<SpecialDateTypeModel> specialDateTypes = specialDateTypeRepo.GetTypes(nbt => ((nbt.BookId.HasValue && nbt.BookId.Value == bookId) || !nbt.BookId.HasValue));

            if (specialDateTypes == null || specialDateTypes.Count == 0)
            {
                return NotFound();
            }

            return Ok(specialDateTypes);
        }

        //Post api/ApiSpecialDateType
        public IHttpActionResult Post([FromBody]SpecialDateTypeModel specialDateType)
        {
            Exception exOut;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ApiHelper.TryExecuteContext(() => specialDateTypeRepo.InsertTypes(new List<SpecialDateTypeModel>() { specialDateType }), out exOut))
            {
                return CreatedAtRoute<SpecialDateTypeModel>("DefaultApi", new { controller = "ApiSpecialDateType", action = "Get", bookId = specialDateType.BookId }, specialDateType);
            }
            else
            {
                return InternalServerError(exOut);
            }
        }

        // PUT api/ApiSpecialDateType
        public IHttpActionResult Put([FromBody]SpecialDateTypeModel pSpecialDateType)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<SpecialDateType> specialdateTypeList = readOnlyRepo.GetCBTypes(nbt => nbt.SpecialDateTpId == pSpecialDateType.SpecialDateTpId && (nbt.BookId.HasValue && nbt.BookId.Value == pSpecialDateType.BookId));

            if (specialdateTypeList == null || specialdateTypeList.Count == 0)
            {
                return NotFound();
            }

            SpecialDateType dbSpecialDateType = specialdateTypeList.SingleOrDefault();

            if (dbSpecialDateType != null && !dbSpecialDateType.Equals(pSpecialDateType))
            {
                if (ApiHelper.TryExecuteContext(() => specialDateTypeRepo.UpdateTypes(dbSpecialDateType, pSpecialDateType), out exOut))
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
            SpecialDateType specialType = null;

            if (bookId <= 0)
            {
                return BadRequest(string.Format("Invalid book id {0}", bookId));
            }

            specialType = readOnlyRepo.GetCBTypes(nb => nb.SpecialDateTpId == typeId && (nb.BookId.HasValue && nb.BookId.Value == bookId)).SingleOrDefault();

            if (specialType == null)
            {
                return NotFound();
            }

            if (specialType.BookId.HasValue)
            {
                specialDateTypeRepo.DeleteTypes(specialType);
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default Special date type.");
            }

            return Ok();
        }
    }
}