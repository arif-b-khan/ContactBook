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
    [RoutePrefix("api/ApiIMType")]
    public class ApiIMTypeController : ApiController
    {
        private IContactBookRepositoryUow _unitofWork;
        private IContactBookRepositoryUow _readOnlyUow;
        private IGenericContextTypes<IMTypeModel, IMType> imTypeRepo;
        private IGenericContextTypes<IMTypeModel, IMType> readOnlyRepo;
        
        public ApiIMTypeController(IContactBookRepositoryUow unitofWork, IContactBookRepositoryUow readOnlyUow)
        {
            _unitofWork = unitofWork;
            _readOnlyUow = readOnlyUow;
            imTypeRepo = new GenericContextTypes<IMTypeModel, IMType>(unitofWork);
            readOnlyRepo = new GenericContextTypes<IMTypeModel, IMType>(_readOnlyUow);
        }

        //Get api/EmailType/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<IMTypeModel>))]
        [BookIdValidationFilter("bookId")]
        public IHttpActionResult Get(long bookId)
        {
            List<IMTypeModel> imTypes = imTypeRepo.GetTypes(imt => ((imt.BookId.HasValue && imt.BookId.Value == bookId) || !imt.BookId.HasValue));

            if (imTypes == null || imTypes.Count == 0)
            {
                return NotFound();
            }

            return Ok(imTypes);
        }

        //Post api/ApiEmailType
        public IHttpActionResult Post([FromBody]IMTypeModel imType)
        {
            Exception exOut;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ApiHelper.TryExecuteContext(() => imTypeRepo.InsertTypes(new List<IMTypeModel>() { imType }), out exOut))
            {
                return CreatedAtRoute<IMTypeModel>("DefaultApi", new { controller = "ApiIMType", action = "Get", bookId = imType.BookId }, imType);
            }
            else
            {
                return InternalServerError(exOut);
            }
        }

        // PUT api/ApiEmailType
        public IHttpActionResult Put([FromBody]IMTypeModel pIMType)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<IMType> imTypeList = readOnlyRepo.GetCBTypes(nbt => nbt.IMTypeId == pIMType.IMTypeId && (nbt.BookId.HasValue && nbt.BookId.Value == pIMType.BookId));

            if (imTypeList == null || imTypeList.Count == 0)
            {
                return NotFound();
            }

            IMType dbIMType = imTypeList.SingleOrDefault();

            if (dbIMType != null && !dbIMType.Equals(pIMType))
            {
                if (ApiHelper.TryExecuteContext(() => imTypeRepo.UpdateTypes(dbIMType, pIMType), out exOut))
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
            IMType imType = null;

            if (bookId <= 0)
            {
                return BadRequest(string.Format("Invalid book id {0}", bookId));
            }

            imType = readOnlyRepo.GetCBTypes(nb => nb.IMTypeId == typeId && (nb.BookId.HasValue && nb.BookId.Value == bookId)).SingleOrDefault();

            if (imType == null)
            {
                return NotFound();
            }

            if (imType.BookId.HasValue)
            {
                imTypeRepo.DeleteTypes(imType);
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default IM type.");
            }

            return Ok();
        }
    }
}