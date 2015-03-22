using ContactBook.Db;
using ContactBook.Db.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactBook.WebApi.Controllers
{
    [RoutePrefix("/api/Sample")]
    public class SampleController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            ContactBookDbRepository<AspNetUser> address =
                new AspNetUser();
            return Ok(address.Get().ToList());
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

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