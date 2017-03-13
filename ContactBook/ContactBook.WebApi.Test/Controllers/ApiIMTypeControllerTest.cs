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
    public class ApiIMTypeControllerTest : IClassFixture<ControllerTestFixtures>
    {
        private CancellationTokenSource cts;
        private List<IMType> emailTypeList = null;

        public ApiIMTypeControllerTest(ControllerTestFixtures fixture)
        {
            ControllerFixture = fixture;
            cts = new CancellationTokenSource(10000);
            emailTypeList = new List<IMType>()
            {
                new IMType(){ IMTypeId = 1, IM_TypeName="Home", BookId=null},
                new IMType(){ IMTypeId = 2, IM_TypeName="Office", BookId=null},
                new IMType(){ IMTypeId = 3, IM_TypeName="abc", BookId=1}
            };
        }

        public ControllerTestFixtures ControllerFixture { get; private set; }

        [Fact]
        public void GetIMTypeShouldReturnNotFound()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(() => ControllerFixture.MockRepository<IMType>(null));

            ApiIMTypeController numberTypeCnt = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberTypeCnt.Request = new HttpRequestMessage();
            numberTypeCnt.Configuration = new HttpConfiguration();

            //act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(numberTypeCnt.Get(1), cts.Token);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetIMTypeShouldReturnNumber()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(
                () => ControllerFixture.MockRepository<IMType>(emailTypeList
                    ));

            ApiIMTypeController imTypeCntr = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            imTypeCntr.Request = new HttpRequestMessage();
            imTypeCntr.Configuration = new HttpConfiguration();

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(imTypeCntr.Get(1), cts.Token);

            List<IMTypeModel> resultType;
            result.TryGetContentValue<List<IMTypeModel>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 3);
        }

        [Fact]
        public void GetIMTypeShouldOnlyDefaultNumbers()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(
                () => ControllerFixture.MockRepository<IMType>(emailTypeList
                    ));
            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);
            ApiIMTypeController IMTypeCntr = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            IMTypeCntr.Request = new HttpRequestMessage();
            IMTypeCntr.Configuration = config;

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(IMTypeCntr.Get(-1), cts.Token);

            List<IMTypeModel> resultType;
            result.TryGetContentValue<List<IMTypeModel>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 2);
        }

        [Fact]
        public void ShouldInsertIMType()
        {
            //Arrange
            List<IMType> imTypeResult = new List<IMType>();
            var imType = new IMTypeModel()
            {
                IMTypeId = 1,
                IMTypeName = "Home",
                BookId = 1
            };
            var cbIMType = new IMType()
            {
                IMTypeId = 1,
                IM_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<IMType>> mockRepo = ControllerFixture.MockRepository<IMType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<IMType>())).Callback<IMType>(
                c =>
                {
                    Mapper.CreateMap<IMType, IMTypeModel>().ForMember(at => at.IMTypeName, cb => cb.MapFrom(m => m.IM_TypeName));

                    imTypeResult.Add(Mapper.Map<IMType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ApiIMTypeController numberController = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);

            //Act
            numberController.Post(imType);

            //Assert
            Assert.NotEmpty(imTypeResult);
        }

        [Fact]
        public void ShouldInsertNumberAndReturnCreatedAndLocation()
        {
            //Arrange
            bool saveCalled = false;
            List<IMType> imTypeResult = new List<IMType>();

            var imType = new IMTypeModel()
            {
                IMTypeId = 1,
                IMTypeName = "Home",
                BookId = 1
            };

            var cbIMType = new IMType()
            {
                IMTypeId = 1,
                IM_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<IMType>> mockRepo = ControllerFixture.MockRepository<IMType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<IMType>())).Callback<IMType>(
                c =>
                {
                    Mapper.CreateMap<IMType, IMTypeModel>().ForMember(at => at.IMTypeName, cb => cb.MapFrom(m => m.IM_TypeName));

                    imTypeResult.Add(Mapper.Map<IMType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(() =>
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

            ApiIMTypeController numberController = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;
            numberController.Url = urlHelperMock.Object;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Post(imType), cts.Token);

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
            List<IMType> imTypeResult = new List<IMType>();

            var imType = new IMTypeModel()
            {
                IMTypeId = 3,
                IMTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<IMType>> mockRepo = ControllerFixture.MockRepositoryNeedList<IMType>(emailTypeList);

            mockRepo.Setup(s => s.Update(It.IsAny<IMType>() , It.IsAny<IMType>())).Callback<IMType, IMType>(
                (a, c) =>
                {
                    IMType upAddr = emailTypeList.Where(cb => cb.IMTypeId == c.IMTypeId).SingleOrDefault();
                    upAddr.IM_TypeName = c.IM_TypeName;
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiIMTypeController numberController = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Put(imType), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.True(emailTypeList.Where(cb => cb.IMTypeId == 3).SingleOrDefault().IM_TypeName == "Updated");
        }

        [Fact]
        public void IMTypeUpdateShouldReturnNotFound()
        {
            //Arrange
            var imType = new IMTypeModel()
            {
                IMTypeId = -3,
                IMTypeName = "Updated",
                BookId = -1
            };

            Mock<IContactBookDbRepository<IMType>> mockRepo = ControllerFixture.MockRepositoryNeedList<IMType>(emailTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiIMTypeController numberController = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Put(imType), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void ShouldDeleteIMTypeAndOked()
        {
            //Arrange
            bool saveCalled = false;
            List<IMType> imTypeResult = new List<IMType>();

            var imType = new IMTypeModel()
            {
                IMTypeId = 3,
                IMTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<IMType>> mockRepo = ControllerFixture.MockRepositoryNeedList<IMType>(emailTypeList);

            mockRepo.Setup(s => s.Delete(It.IsAny<IMType>())).Callback<IMType>(
                c =>
                {
                    IMType delAddr = emailTypeList.Where(cb => cb.IMTypeId == c.IMTypeId).SingleOrDefault();
                    emailTypeList.Remove(delAddr);
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiIMTypeController numberController = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(imType.BookId.Value, imType.IMTypeId), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.Null(emailTypeList.Where(cb => cb.IMTypeId == 3).SingleOrDefault());
        }

        [Fact]
        public void IMTypeDeleteReturnNotFound()
        {
            //Arrange
            //bool saveCalled = false;
            List<IMType> imTypeResult = new List<IMType>();

            var imType = new IMTypeModel()
            {
                IMTypeId = 3,
                IMTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<IMType>> mockRepo = ControllerFixture.MockRepositoryNeedList<IMType>(emailTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<IMType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiIMTypeController numberController = new ApiIMTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(2, 1), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }
    }
}