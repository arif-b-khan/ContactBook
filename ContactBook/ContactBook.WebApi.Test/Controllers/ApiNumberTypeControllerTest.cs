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
    public class ApiNumberTypeControllerTest : IClassFixture<ControllerTestFixtures>
    {
        private CancellationTokenSource cts;
        private List<NumberType> NumberTypeList = null;

        public ApiNumberTypeControllerTest(ControllerTestFixtures fixture)
        {
            ControllerFixture = fixture;
            cts = new CancellationTokenSource(10000);
            NumberTypeList = new List<NumberType>()
            {
                new NumberType(){ NumberTypeId = 1, Number_TypeName="Home", BookId=null},
                new NumberType(){ NumberTypeId = 2, Number_TypeName="Office", BookId=null},
                new NumberType(){ NumberTypeId = 3, Number_TypeName="abc", BookId=1}
            };
        }

        public ControllerTestFixtures ControllerFixture { get; private set; }

        [Fact]
        public void GetNumberTypeShouldReturnNotFound()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(
                () => ControllerFixture.MockRepository<NumberType>(null));

            ApiNumberTypeController numberTypeCnt = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberTypeCnt.Request = new HttpRequestMessage();
            numberTypeCnt.Configuration = new HttpConfiguration();

            //act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(numberTypeCnt.Get(1), cts.Token);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetNumberTypeShouldReturnNumber()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(
                () => ControllerFixture.MockRepository<NumberType>(NumberTypeList
                    ));

            ApiNumberTypeController NumberTypeCntr = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            NumberTypeCntr.Request = new HttpRequestMessage();
            NumberTypeCntr.Configuration = new HttpConfiguration();

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(NumberTypeCntr.Get(1), cts.Token);

            List<NumberTypeModel> resultType;
            result.TryGetContentValue<List<NumberTypeModel>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 3);
        }

        [Fact]
        public void GetNumberTypeShouldOnlyDefaultNumbers()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(
                () => ControllerFixture.MockRepository<NumberType>(NumberTypeList
                    ));
            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);
            ApiNumberTypeController NumberTypeCntr = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            NumberTypeCntr.Request = new HttpRequestMessage();
            NumberTypeCntr.Configuration = config;

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(NumberTypeCntr.Get(-1), cts.Token);

            List<NumberTypeModel> resultType;
            result.TryGetContentValue<List<NumberTypeModel>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 2);
        }

        [Fact]
        public void ShouldInsertNumberType()
        {
            //Arrange
            List<NumberType> NumberTypeResult = new List<NumberType>();
            var NumberType = new NumberTypeModel()
            {
                NumberTypeId = 1,
                NumberTypeName = "Home",
                BookId = 1
            };
            var cbNumberType = new NumberType()
            {
                NumberTypeId = 1,
                Number_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<NumberType>> mockRepo = ControllerFixture.MockRepository<NumberType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<NumberType>())).Callback<NumberType>(
                c =>
                {
                    Mapper.CreateMap<NumberType, NumberTypeModel>().ForMember(at => at.NumberTypeName, cb => cb.MapFrom(m => m.Number_TypeName));

                    NumberTypeResult.Add(Mapper.Map<NumberType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ApiNumberTypeController numberController = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);

            //Act
            numberController.Post(NumberType);

            //Assert
            Assert.NotEmpty(NumberTypeResult);
        }

        [Fact]
        public void ShouldInsertNumberAndReturnCreatedAndLocation()
        {
            //Arrange
            bool saveCalled = false;
            List<NumberType> NumberTypeResult = new List<NumberType>();

            var NumberType = new NumberTypeModel()
            {
                NumberTypeId = 1,
                NumberTypeName = "Home",
                BookId = 1
            };

            var cbNumberType = new NumberType()
            {
                NumberTypeId = 1,
                Number_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<NumberType>> mockRepo = ControllerFixture.MockRepository<NumberType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<NumberType>())).Callback<NumberType>(
                c =>
                {
                    Mapper.CreateMap<NumberType, NumberTypeModel>().ForMember(at => at.NumberTypeName, cb => cb.MapFrom(m => m.Number_TypeName));

                    NumberTypeResult.Add(Mapper.Map<NumberType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(() =>
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

            ApiNumberTypeController numberController = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;
            numberController.Url = urlHelperMock.Object;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Post(NumberType), cts.Token);

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
            List<NumberType> NumberTypeResult = new List<NumberType>();

            var NumberType = new NumberTypeModel()
            {
                NumberTypeId = 3,
                NumberTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<NumberType>> mockRepo = ControllerFixture.MockRepositoryNeedList<NumberType>(NumberTypeList);

            mockRepo.Setup(s => s.Update(It.IsAny<NumberType>(), It.IsAny<NumberType>())).Callback<NumberType, NumberType>(
                (a, c) =>
                {
                    NumberType upAddr = NumberTypeList.Where(cb => cb.NumberTypeId == c.NumberTypeId).SingleOrDefault();
                    upAddr.Number_TypeName = c.Number_TypeName;
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiNumberTypeController numberController = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Put(NumberType), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.True(NumberTypeList.Where(cb => cb.NumberTypeId == 3).SingleOrDefault().Number_TypeName == "Updated");
        }

        [Fact]
        public void NumberTypeUpdateShouldReturnNotFound()
        {
            //Arrange
            var NumberType = new NumberTypeModel()
            {
                NumberTypeId = -3,
                NumberTypeName = "Updated",
                BookId = -1
            };

            Mock<IContactBookDbRepository<NumberType>> mockRepo = ControllerFixture.MockRepositoryNeedList<NumberType>(NumberTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiNumberTypeController numberController = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Put(NumberType), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void ShouldDeleteNumberTypeAndOked()
        {
            //Arrange
            bool saveCalled = false;
            List<NumberType> NumberTypeResult = new List<NumberType>();

            var NumberType = new NumberTypeModel()
            {
                NumberTypeId = 3,
                NumberTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<NumberType>> mockRepo = ControllerFixture.MockRepositoryNeedList<NumberType>(NumberTypeList);

            mockRepo.Setup(s => s.Delete(It.IsAny<NumberType>())).Callback<NumberType>(
                c =>
                {
                    NumberType delAddr = NumberTypeList.Where(cb => cb.NumberTypeId == c.NumberTypeId).SingleOrDefault();
                    NumberTypeList.Remove(delAddr);
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiNumberTypeController numberController = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(NumberType.BookId.Value, NumberType.NumberTypeId), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.Null(NumberTypeList.Where(cb => cb.NumberTypeId == 3).SingleOrDefault());
        }

        [Fact]
        public void NumberTypeDeleteReturnNotFound()
        {
            //Arrange
            //bool saveCalled = false;
            List<NumberType> NumberTypeResult = new List<NumberType>();

            var NumberType = new NumberTypeModel()
            {
                NumberTypeId = 3,
                NumberTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<NumberType>> mockRepo = ControllerFixture.MockRepositoryNeedList<NumberType>(NumberTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<NumberType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiNumberTypeController numberController = new ApiNumberTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(2, 1), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }
    }
}