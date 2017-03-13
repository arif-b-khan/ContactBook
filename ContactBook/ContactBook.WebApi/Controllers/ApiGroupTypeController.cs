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
    [RoutePrefix("api/ApiGroupType")]
    public class ApiGroupTypeController : ApiController
    {
        private IGenericContextTypes<GroupTypeModel, GroupType> groupTypeRepo;
        
        public ApiGroupTypeController(IContactBookRepositoryUow unitofWork)
        {
            groupTypeRepo = new GenericContextTypes<GroupTypeModel, GroupType>(unitofWork);
        }

        //Get api/GroupType/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<GroupTypeModel>))]
        [BookIdValidationFilter("bookId")]
        public IHttpActionResult Get(long bookId)
        {
            List<GroupTypeModel> groupTypes = groupTypeRepo.GetTypes(nbt => ((nbt.BookId.HasValue && nbt.BookId.Value == bookId) || !nbt.BookId.HasValue));

            if (groupTypes == null || groupTypes.Count == 0)
            {
                return NotFound();
            }

            return Ok(groupTypes);
        }

        //Post api/ApiGroupType
        public IHttpActionResult Post([FromBody]GroupTypeModel groupType)
        {
            Exception exOut;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ApiHelper.TryExecuteContext(() => groupTypeRepo.InsertTypes(new List<GroupTypeModel>() { groupType }), out exOut))
            {
                return CreatedAtRoute<GroupTypeModel>("DefaultApi", new { controller = "ApiGroupType", action = "Get", bookId = groupType.BookId }, groupType);
            }
            else
            {
                return InternalServerError(exOut);
            }
        }

        // PUT api/ApiGroupType
        public IHttpActionResult Put([FromBody]GroupTypeModel pGroupType)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<GroupType> groupTypeList = groupTypeRepo.GetCBTypes(nbt => nbt.GroupId == pGroupType.GroupId && (nbt.BookId.HasValue && nbt.BookId.Value == pGroupType.BookId));

            if (groupTypeList == null || groupTypeList.Count == 0)
            {
                return NotFound();
            }

            GroupType dbGroupType = groupTypeList.SingleOrDefault();

            if (dbGroupType != null && !dbGroupType.Equals(pGroupType))
            {
                if (ApiHelper.TryExecuteContext(() => groupTypeRepo.UpdateTypes(dbGroupType, pGroupType), out exOut))
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
            GroupType groupType = null;

            if (bookId <= 0)
            {
                return BadRequest(string.Format("Invalid book id {0}", bookId));
            }

            groupType = groupTypeRepo.GetCBTypes(nb => nb.GroupId == typeId && (nb.BookId.HasValue && nb.BookId.Value == bookId)).SingleOrDefault();

            if (groupType == null)
            {
                return NotFound();
            }

            if (groupType.BookId.HasValue)
            {
                groupTypeRepo.DeleteTypes(groupType);
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default Group type.");
            }

            return Ok();
        }
    }
}