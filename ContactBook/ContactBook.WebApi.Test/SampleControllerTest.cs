using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using ContactBook.WebApi.Controllers;
using Xunit;
using ContactBook.Db;

namespace ContactBook.WebApi.Test
{
    public class SampleControllerTest
    {
        [Fact]
        public void GetTest()
        {
            ////prepare
            //var fact = new List<AspNetUser> { };
            //var sampleCntr = new SampleController();
            ////act
            //OkNegotiatedContentResult<List<AspNetUser>> actual = sampleCntr.Get() as OkNegotiatedContentResult<List<AspNetUser>>;

            ////assert
            //Assert.True(fact.SequenceEqual(actual.Content), "collections equal");
        }
    }
}
