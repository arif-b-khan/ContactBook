using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using ContactBook.WebApi.Controllers;
using ContactBook.WebApi.Test.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Routing;
using Xunit;

namespace ContactBook.WebApi.Test.Controllers
{
    public class ApiEmailTypeControllerTest : IClassFixture<ControllerTestFixtures>
    {
        private CancellationTokenSource cts;
        private List<CB_EmailType> emailTypeList = null;

        public ApiEmailTypeControllerTest(ControllerTestFixtures fixture)
        {
            ControllerFixture = fixture;
            cts = new CancellationTokenSource(10000);
            emailTypeList = new List<CB_EmailType>()
            {
                new CB_EmailType(){ EmailTypeId = 1, Email_TypeName="Home", BookId=null},
                new CB_EmailType(){ EmailTypeId = 2, Email_TypeName="Office", BookId=null},
                new CB_EmailType(){ EmailTypeId = 3, Email_TypeName="abc", BookId=1}
            };
        }

        public ControllerTestFixtures ControllerFixture { get; private set; }

        [Fact]
        public void GetEmailTypeShouldReturnNotFound()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(() => ControllerFixture.MockRepository<CB_EmailType>(null));

            ApiEmailTypeController numberTypeCnt = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            numberTypeCnt.Request = new HttpRequestMessage();
            numberTypeCnt.Configuration = new HttpConfiguration();

            //act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(numberTypeCnt.Get(1), cts.Token);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetEmailTypeShouldReturnNumber()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(
                () => ControllerFixture.MockRepository<CB_EmailType>(emailTypeList
                    ));

            ApiEmailTypeController emailTypeCntr = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            emailTypeCntr.Request = new HttpRequestMessage();
            emailTypeCntr.Configuration = new HttpConfiguration();

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(emailTypeCntr.Get(1), cts.Token);

            List<EmailType> resultType;
            result.TryGetContentValue<List<EmailType>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 3);
        }

        [Fact]
        public void GetEmailTypeShouldOnlyDefaultNumbers()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(
                () => ControllerFixture.MockRepository<CB_EmailType>(emailTypeList
                    ));
            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);
            ApiEmailTypeController EmailTypeCntr = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            EmailTypeCntr.Request = new HttpRequestMessage();
            EmailTypeCntr.Configuration = config;

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(EmailTypeCntr.Get(-1), cts.Token);

            List<EmailType> resultType;
            result.TryGetContentValue<List<EmailType>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 2);
        }

        [Fact]
        public void ShouldInsertEmailType()
        {
            //Arrange
            List<EmailType> emailTypeResult = new List<EmailType>();
            var emailType = new EmailType()
            {
                EmailTypeId = 1,
                EmailTypeName = "Home",
                BookId = 1
            };
            var cbEmailType = new CB_EmailType()
            {
                EmailTypeId = 1,
                Email_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_EmailType>> mockRepo = ControllerFixture.MockRepository<CB_EmailType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<CB_EmailType>())).Callback<CB_EmailType>(
                c =>
                {
                    Mapper.CreateMap<CB_EmailType, EmailType>().ForMember(at => at.EmailTypeName, cb => cb.MapFrom(m => m.Email_TypeName));

                    emailTypeResult.Add(Mapper.Map<EmailType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ApiEmailTypeController numberController = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);

            //Act
            numberController.Post(emailType);

            //Assert
            Assert.NotEmpty(emailTypeResult);
        }

        [Fact]
        public void ShouldInsertNumberAndReturnCreatedAndLocation()
        {
            //Arrange
            bool saveCalled = false;
            List<EmailType> emailTypeResult = new List<EmailType>();

            var emailType = new EmailType()
            {
                EmailTypeId = 1,
                EmailTypeName = "Home",
                BookId = 1
            };

            var cbEmailType = new CB_EmailType()
            {
                EmailTypeId = 1,
                Email_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_EmailType>> mockRepo = ControllerFixture.MockRepository<CB_EmailType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<CB_EmailType>())).Callback<CB_EmailType>(
                c =>
                {
                    Mapper.CreateMap<CB_EmailType, EmailType>().ForMember(at => at.EmailTypeName, cb => cb.MapFrom(m => m.Email_TypeName));

                    emailTypeResult.Add(Mapper.Map<EmailType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            Mock<UrlHelper> urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost");
            urlHelperMock.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>())).Returns("http://localhost");

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiEmailTypeController numberController = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;
            numberController.Url = urlHelperMock.Object;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Post(emailType), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(resMsg.Headers.Location);
        }

        [Fact]
        public void ShouldUpdateNumberOk()
        {
            //Arrange
            bool saveCalled = false;
            List<EmailType> emailTypeResult = new List<EmailType>();

            var emailType = new EmailType()
            {
                EmailTypeId = 3,
                EmailTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_EmailType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_EmailType>(emailTypeList);

            mockRepo.Setup(s => s.Update(It.IsAny<CB_EmailType>())).Callback<CB_EmailType>(
                c =>
                {
                    CB_EmailType upAddr = emailTypeList.Where(cb => cb.EmailTypeId == c.EmailTypeId).SingleOrDefault();
                    upAddr.Email_TypeName = c.Email_TypeName;
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiEmailTypeController numberController = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Put(emailType), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.True(emailTypeList.Where(cb => cb.EmailTypeId == 3).SingleOrDefault().Email_TypeName == "Updated");
        }

        [Fact]
        public void EmailTypeUpdateShouldReturnNotFound()
        {
            //Arrange
            var emailType = new EmailType()
            {
                EmailTypeId = -3,
                EmailTypeName = "Updated",
                BookId = -1
            };

            Mock<IContactBookDbRepository<CB_EmailType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_EmailType>(emailTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiEmailTypeController numberController = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Put(emailType), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void ShouldDeleteEmailTypeAndOked()
        {
            //Arrange
            bool saveCalled = false;
            List<EmailType> emailTypeResult = new List<EmailType>();

            var emailType = new EmailType()
            {
                EmailTypeId = 3,
                EmailTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_EmailType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_EmailType>(emailTypeList);

            mockRepo.Setup(s => s.Delete(It.IsAny<CB_EmailType>())).Callback<CB_EmailType>(
                c =>
                {
                    CB_EmailType delAddr = emailTypeList.Where(cb => cb.EmailTypeId == c.EmailTypeId).SingleOrDefault();
                    emailTypeList.Remove(delAddr);
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiEmailTypeController numberController = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(emailType.BookId.Value, emailType.EmailTypeId), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.Null(emailTypeList.Where(cb => cb.EmailTypeId == 3).SingleOrDefault());
        }

        [Fact]
        public void EmailTypeDeleteReturnNotFound()
        {
            //Arrange
            bool saveCalled = false;
            List<EmailType> emailTypeResult = new List<EmailType>();

            var emailType = new EmailType()
            {
                EmailTypeId = 3,
                EmailTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_EmailType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_EmailType>(emailTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_EmailType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiEmailTypeController numberController = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(2, 1), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }
    }
}