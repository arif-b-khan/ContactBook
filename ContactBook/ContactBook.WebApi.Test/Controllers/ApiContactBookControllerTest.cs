using ContactBook.Db.Data;
using ContactBook.Domain.Models;
using ContactBook.WebApi.Controllers;
using ContactBook.WebApi.Test.Fixtures;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Xunit;

namespace ContactBook.WebApi.Test.Controllers
{
    public class ApiContactBookControllerTest : IClassFixture<ControllerTestFixtures>
    {
        private CancellationTokenSource cts;

        public ApiContactBookControllerTest(ControllerTestFixtures fixture)
        {
            ControllerFixture = fixture;
            cts = new CancellationTokenSource(10000);
        }

        public ControllerTestFixtures ControllerFixture { get; private set; }

        [Fact]
        public void GetContactBookShouldReturnBadRequest()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_ContactBook>()).Returns(
                () => ControllerFixture.MockRepository<CB_ContactBook>(null)
                );
            ApiContactBookController cbController = new ApiContactBookController(ControllerFixture.MockUnitOfWork.Object);
            cbController.Request = new HttpRequestMessage();
            cbController.Configuration = new HttpConfiguration();

            //Act
            IHttpActionResult result = cbController.GetContactBook("");
            HttpResponseMessage message = result.ExecuteAsync(cts.Token).Result;

            //Assert
            Assert.True(message.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetContactBookShouldReturnContactBook()
        {
            //Arrange
            ContactBookInfo cbInfo;
            var contactBookList = new List<CB_ContactBook>()
                {
                    new CB_ContactBook(){BookId = 1, BookName="Temp1", Username="axkhan2"},
                                        new CB_ContactBook(){BookId = 2, BookName="Temp2", Username="axkhan1"}
                };
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_ContactBook>()).Returns(
                () => ControllerFixture.MockRepository<CB_ContactBook>(contactBookList
                )
                );
            ApiContactBookController cbController = new ApiContactBookController(ControllerFixture.MockUnitOfWork.Object);
            cbController.Request = new HttpRequestMessage();
            cbController.Configuration = new HttpConfiguration();

            //Act
            IHttpActionResult result = cbController.GetContactBook("axkhan2");
            HttpResponseMessage message = result.ExecuteAsync(cts.Token).Result;

            //Assert
            Assert.True(message.StatusCode == System.Net.HttpStatusCode.OK);
            message.TryGetContentValue<ContactBookInfo>(out cbInfo);
            Assert.NotNull(cbInfo);
        }
    }
}