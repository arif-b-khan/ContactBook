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
        private List<EmailType> emailTypeList = null;

        public ApiEmailTypeControllerTest(ControllerTestFixtures fixture)
        {
            ControllerFixture = fixture;
            cts = new CancellationTokenSource(10000);
            emailTypeList = new List<EmailType>()
            {
                new EmailType(){ EmailTypeId = 1, Email_TypeName="Home", BookId=null},
                new EmailType(){ EmailTypeId = 2, Email_TypeName="Office", BookId=null},
                new EmailType(){ EmailTypeId = 3, Email_TypeName="abc", BookId=1}
            };
        }

        public ControllerTestFixtures ControllerFixture { get; private set; }

        [Fact]
        public void GetEmailTypeShouldReturnNotFound()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(() => ControllerFixture.MockRepository<EmailType>(null));

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
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(
                () => ControllerFixture.MockRepository<EmailType>(emailTypeList
                    ));

            ApiEmailTypeController emailTypeCntr = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            emailTypeCntr.Request = new HttpRequestMessage();
            emailTypeCntr.Configuration = new HttpConfiguration();

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(emailTypeCntr.Get(1), cts.Token);

            List<EmailTypeModel> resultType;
            result.TryGetContentValue<List<EmailTypeModel>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 3);
        }

        [Fact]
        public void GetEmailTypeShouldOnlyDefaultNumbers()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(
                () => ControllerFixture.MockRepository<EmailType>(emailTypeList
                    ));
            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);
            ApiEmailTypeController EmailTypeCntr = new ApiEmailTypeController(ControllerFixture.MockUnitOfWork.Object);
            EmailTypeCntr.Request = new HttpRequestMessage();
            EmailTypeCntr.Configuration = config;

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(EmailTypeCntr.Get(-1), cts.Token);

            List<EmailTypeModel> resultType;
            result.TryGetContentValue<List<EmailTypeModel>>(out resultType);

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
            var emailType = new EmailTypeModel()
            {
                EmailTypeId = 1,
                EmailTypeName = "Home",
                BookId = 1
            };
            var cbEmailType = new EmailType()
            {
                EmailTypeId = 1,
                Email_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<EmailType>> mockRepo = ControllerFixture.MockRepository<EmailType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<EmailType>())).Callback<EmailType>(
                c =>
                {
                    Mapper.CreateMap<EmailType, EmailTypeModel>().ForMember(at => at.EmailTypeName, cb => cb.MapFrom(m => m.Email_TypeName));

                    emailTypeResult.Add(Mapper.Map<EmailType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(() =>
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

            var emailType = new EmailTypeModel()
            {
                EmailTypeId = 1,
                EmailTypeName = "Home",
                BookId = 1
            };

            var cbEmailType = new EmailType()
            {
                EmailTypeId = 1,
                Email_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<EmailType>> mockRepo = ControllerFixture.MockRepository<EmailType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<EmailType>())).Callback<EmailType>(
                c =>
                {
                    Mapper.CreateMap<EmailType, EmailTypeModel>().ForMember(at => at.EmailTypeName, cb => cb.MapFrom(m => m.Email_TypeName));

                    emailTypeResult.Add(Mapper.Map<EmailType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(() =>
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

            var emailType = new EmailTypeModel()
            {
                EmailTypeId = 3,
                EmailTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<EmailType>> mockRepo = ControllerFixture.MockRepositoryNeedList<EmailType>(emailTypeList);

            mockRepo.Setup(s => s.Update(It.IsAny<EmailType>(), It.IsAny<EmailType>())).Callback<EmailType, EmailType>(
                (a, c) =>
                {
                    EmailType upAddr = emailTypeList.Where(cb => cb.EmailTypeId == c.EmailTypeId).SingleOrDefault();
                    upAddr.Email_TypeName = c.Email_TypeName;
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(() =>
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
            var emailType = new EmailTypeModel()
            {
                EmailTypeId = -3,
                EmailTypeName = "Updated",
                BookId = -1
            };

            Mock<IContactBookDbRepository<EmailType>> mockRepo = ControllerFixture.MockRepositoryNeedList<EmailType>(emailTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(() =>
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

            var emailType = new EmailTypeModel()
            {
                EmailTypeId = 3,
                EmailTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<EmailType>> mockRepo = ControllerFixture.MockRepositoryNeedList<EmailType>(emailTypeList);

            mockRepo.Setup(s => s.Delete(It.IsAny<EmailType>())).Callback<EmailType>(
                c =>
                {
                    EmailType delAddr = emailTypeList.Where(cb => cb.EmailTypeId == c.EmailTypeId).SingleOrDefault();
                    emailTypeList.Remove(delAddr);
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(() =>
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
            //bool saveCalled = false;
            List<EmailType> emailTypeResult = new List<EmailType>();

            var emailType = new EmailTypeModel()
            {
                EmailTypeId = 3,
                EmailTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<EmailType>> mockRepo = ControllerFixture.MockRepositoryNeedList<EmailType>(emailTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<EmailType>()).Returns(() =>
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