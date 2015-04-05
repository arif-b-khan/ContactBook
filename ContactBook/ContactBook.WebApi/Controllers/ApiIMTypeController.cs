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

namespace ContactBook.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/ApiIMType")]
    public class ApiIMTypeController : ApiController
    {
        private IContactBookRepositoryUow _unitofWork;
        private IContactBookRepositoryUow _readOnlyUow;
        private IGenericContextTypes<IMType, CB_IMType> imTypeRepo;
        private IGenericContextTypes<IMType, CB_IMType> readOnlyRepo;

        public ApiIMTypeController()
            : this(DependencyFactory.Resolve<IContactBookRepositoryUow>(), DependencyFactory.Resolve<IContactBookRepositoryUow>())
        {
        }

        public ApiIMTypeController(IContactBookRepositoryUow unitofWork, IContactBookRepositoryUow readOnlyUow)
        {
            _unitofWork = unitofWork;
            _readOnlyUow = readOnlyUow;
            imTypeRepo = new GenericContextTypes<IMType, CB_IMType>(unitofWork);
            readOnlyRepo = new GenericContextTypes<IMType, CB_IMType>(_readOnlyUow);
        }

        //Get api/EmailType/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<IMType>))]
        public IHttpActionResult Get(long bookId)
        {
            List<IMType> imTypes = imTypeRepo.GetTypes(imt => ((imt.BookId.HasValue && imt.BookId.Value == bookId) || !imt.BookId.HasValue));

            if (imTypes == null || imTypes.Count == 0)
            {
                return NotFound();
            }

            return Ok(imTypes);
        }

        //Post api/ApiEmailType
        public IHttpActionResult Post([FromBody]IMType imType)
        {
            Exception exOut;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ApiHelper.TryExecuteContext(() => imTypeRepo.InsertTypes(new List<IMType>() { imType }), out exOut))
            {
                return CreatedAtRoute<IMType>("DefaultApi", new { controller = "ApiIMType", action = "Get", bookId = imType.BookId }, imType);
            }
            else
            {
                return InternalServerError(exOut);
            }
        }

        // PUT api/ApiEmailType
        public IHttpActionResult Put([FromBody]IMType pIMType)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<IMType> imTypeList = readOnlyRepo.GetTypes(nbt => nbt.IMTypeId == pIMType.IMTypeId && (nbt.BookId.HasValue && nbt.BookId.Value == pIMType.BookId));

            if (imTypeList == null || imTypeList.Count == 0)
            {
                return NotFound();
            }

            IMType dbIMType = imTypeList.SingleOrDefault();

            if (dbIMType != null && !dbIMType.Equals(pIMType))
            {
                if (ApiHelper.TryExecuteContext(() => imTypeRepo.UpdateTypes(new List<IMType>() { pIMType }), out exOut))
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

            imType = readOnlyRepo.GetTypes(nb => nb.IMTypeId == typeId && (nb.BookId.HasValue && nb.BookId.Value == bookId)).SingleOrDefault();

            if (imType == null)
            {
                return NotFound();
            }

            if (imType.BookId.HasValue)
            {
                imTypeRepo.DeleteTypes(new List<IMType>() { imType });
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default IM type.");
            }

            return Ok();
        }
    }
}