using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using ContactBook.WebApi.Controllers;
using Xunit;

namespace ContactBook.WebApi.Test
{
    public class SampleControllerTest
    {
        [Fact]
        public void GetTest()
        {
            //prepare
            var fact = new List<string> { "value1", "value2" };
            var sampleCntr = new SampleController();
            //act
            OkNegotiatedContentResult<string[]> actual = sampleCntr.Get() as OkNegotiatedContentResult<string[]>;

            //assert
            Assert.True(fact.SequenceEqual(actual.Content), "collections equal");
        }
    }
}
