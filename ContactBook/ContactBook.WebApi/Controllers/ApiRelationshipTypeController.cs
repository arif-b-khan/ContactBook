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
    [RoutePrefix("api/ApiRelationshipType")]
    public class ApiRelationshipTypeController : ApiController
    {
        private IContactBookRepositoryUow _unitofWork;
        private IContactBookRepositoryUow _readOnlyUow;
        private IGenericContextTypes<RelationshipTypeModel, RelationshipType> relationshipTypeRepo;
        private IGenericContextTypes<RelationshipTypeModel, RelationshipType> readOnlyRepo;
        
        public ApiRelationshipTypeController(IContactBookRepositoryUow unitofWork, IContactBookRepositoryUow readOnlyUow)
        {
            _unitofWork = unitofWork;
            _readOnlyUow = readOnlyUow;
            relationshipTypeRepo = new GenericContextTypes<RelationshipTypeModel, RelationshipType>(unitofWork);
            readOnlyRepo = new GenericContextTypes<RelationshipTypeModel, RelationshipType>(_readOnlyUow);
        }

        //Get api/RelationshipType/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<RelationshipTypeModel>))]
        [BookIdValidationFilter("bookId")]
        public IHttpActionResult Get(long bookId)
        {
            List<RelationshipTypeModel> relationshipTypes = relationshipTypeRepo.GetTypes(nbt => ((nbt.BookId.HasValue && nbt.BookId.Value == bookId) || !nbt.BookId.HasValue));

            if (relationshipTypes == null || relationshipTypes.Count == 0)
            {
                return NotFound();
            }

            return Ok(relationshipTypes);
        }

        //Post api/ApiRelationshipType
        public IHttpActionResult Post([FromBody]RelationshipTypeModel relationshipType)
        {
            Exception exOut;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ApiHelper.TryExecuteContext(() => relationshipTypeRepo.InsertTypes(new List<RelationshipTypeModel>() { relationshipType }), out exOut))
            {
                return CreatedAtRoute<RelationshipTypeModel>("DefaultApi", new { controller = "ApiRelationshipType", action = "Get", bookId = relationshipType.BookId }, relationshipType);
            }
            else
            {
                return InternalServerError(exOut);
            }
        }

        // PUT api/ApiRelationshipType
        public IHttpActionResult Put([FromBody]RelationshipTypeModel pRelationshipType)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<RelationshipType> relatiionshipTypeList = readOnlyRepo.GetCBTypes(nbt => nbt.RelationshipTypeId == pRelationshipType.RelationshipTypeId && (nbt.BookId.HasValue && nbt.BookId.Value == pRelationshipType.BookId));

            if (relatiionshipTypeList == null || relatiionshipTypeList.Count == 0)
            {
                return NotFound();
            }

            RelationshipType dbRelationshipType = relatiionshipTypeList.SingleOrDefault();

            if (dbRelationshipType != null && !dbRelationshipType.Equals(pRelationshipType))
            {
                if (ApiHelper.TryExecuteContext(() => relationshipTypeRepo.UpdateTypes(dbRelationshipType, pRelationshipType), out exOut))
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
            RelationshipType relationshipType = null;

            if (bookId <= 0)
            {
                return BadRequest(string.Format("Invalid book id {0}", bookId));
            }

            relationshipType = readOnlyRepo.GetCBTypes(nb => nb.RelationshipTypeId == typeId && (nb.BookId.HasValue && nb.BookId.Value == bookId)).SingleOrDefault();

            if (relationshipType == null)
            {
                return NotFound();
            }

            if (relationshipType.BookId.HasValue)
            {
                relationshipTypeRepo.DeleteTypes(relationshipType);
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default Relationship type.");
            }

            return Ok();
        }
    }
}