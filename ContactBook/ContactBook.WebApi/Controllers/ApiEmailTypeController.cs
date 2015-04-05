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
    [RoutePrefix("api/ApiEmailType")]
    public class ApiEmailTypeController : ApiController
    {
        private IContactBookRepositoryUow _unitofWork;
        private IContactBookRepositoryUow _readOnlyUow;
        private IGenericContextTypes<EmailType, CB_EmailType> emailTypeRepo;
        private IGenericContextTypes<EmailType, CB_EmailType> readOnlyRepo;

        public ApiEmailTypeController()
            : this(DependencyFactory.Resolve<IContactBookRepositoryUow>(), DependencyFactory.Resolve<IContactBookRepositoryUow>())
        {
        }

        public ApiEmailTypeController(IContactBookRepositoryUow unitofWork, IContactBookRepositoryUow readOnlyUow)
        {
            _unitofWork = unitofWork;
            _readOnlyUow = readOnlyUow;
            emailTypeRepo = new GenericContextTypes<EmailType, CB_EmailType>(unitofWork);
            readOnlyRepo = new GenericContextTypes<EmailType, CB_EmailType>(_readOnlyUow);
        }

        //Get api/EmailType/1
        [Route("{bookId}")]
        [ResponseType(typeof(List<EmailType>))]
        public IHttpActionResult Get(long bookId)
        {
            List<EmailType> emailTypes = emailTypeRepo.GetTypes(nbt => ((nbt.BookId.HasValue && nbt.BookId.Value == bookId) || !nbt.BookId.HasValue));

            if (emailTypes == null || emailTypes.Count == 0)
            {
                return NotFound();
            }

            return Ok(emailTypes);
        }

        //Post api/ApiEmailType
        public IHttpActionResult Post([FromBody]EmailType emailType)
        {
            Exception exOut;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ApiHelper.TryExecuteContext(() => emailTypeRepo.InsertTypes(new List<EmailType>() { emailType }), out exOut))
            {
                return CreatedAtRoute<EmailType>("DefaultApi", new { controller = "ApiEmailType", action = "Get", bookId = emailType.BookId }, emailType);
            }
            else
            {
                return InternalServerError(exOut);
            }
        }

        // PUT api/ApiEmailType
        public IHttpActionResult Put([FromBody]EmailType pEmailType)
        {
            Exception exOut;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<EmailType> emailTypeList = readOnlyRepo.GetTypes(nbt => nbt.EmailTypeId == pEmailType.EmailTypeId && (nbt.BookId.HasValue && nbt.BookId.Value == pEmailType.BookId));

            if (emailTypeList == null || emailTypeList.Count == 0)
            {
                return NotFound();
            }

            EmailType dbEmailType = emailTypeList.SingleOrDefault();

            if (dbEmailType != null && !dbEmailType.Equals(pEmailType))
            {
                if (ApiHelper.TryExecuteContext(() => emailTypeRepo.UpdateTypes(new List<EmailType>() { pEmailType }), out exOut))
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
            EmailType emailType = null;

            if (bookId <= 0)
            {
                return BadRequest(string.Format("Invalid book id {0}", bookId));
            }

            emailType = readOnlyRepo.GetTypes(nb => nb.EmailTypeId == typeId && (nb.BookId.HasValue && nb.BookId.Value == bookId)).SingleOrDefault();

            if (emailType == null)
            {
                return NotFound();
            }

            if (emailType.BookId.HasValue)
            {
                emailTypeRepo.DeleteTypes(new List<EmailType>() { emailType });
            }
            else
            {
                return BadRequest("Invalid operation. You're trying to delete default Email type.");
            }

            return Ok();
        }
    }
}