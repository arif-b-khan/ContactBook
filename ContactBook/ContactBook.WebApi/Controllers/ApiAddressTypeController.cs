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

namespace ContactBook.WebApi.Controllers
{
    public class ApiAddressTypeController : ApiController
    {
        // GET api/<controller>
        //[ResponseType(typeof(List<MdlAddressType>))]
        //public IEnumerable<string> Get()
        //{
        //    using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
        //    {

        //    }
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
            
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}