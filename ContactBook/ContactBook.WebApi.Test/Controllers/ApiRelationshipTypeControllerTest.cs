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
    public class ApiRelationshipTypeControllerTest : IClassFixture<ControllerTestFixtures>
    {
        private CancellationTokenSource cts;
        private List<RelationshipType> relationshipTypeList = null;

        public ApiRelationshipTypeControllerTest(ControllerTestFixtures fixture)
        {
            ControllerFixture = fixture;
            cts = new CancellationTokenSource(10000);
            relationshipTypeList = new List<RelationshipType>()
            {
                new RelationshipType(){ RelationshipTypeId = 1, Relationship_TypeName="Home", BookId=null},
                new RelationshipType(){ RelationshipTypeId = 2, Relationship_TypeName="Office", BookId=null},
                new RelationshipType(){ RelationshipTypeId = 3, Relationship_TypeName="abc", BookId=1}
            };
        }

        public ControllerTestFixtures ControllerFixture { get; private set; }

        [Fact]
        public void GetRelationshipTypeShouldReturnNotFound()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(() => ControllerFixture.MockRepository<RelationshipType>(null));

            ApiRelationshipTypeController typeCnt = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            typeCnt.Request = new HttpRequestMessage();
            typeCnt.Configuration = new HttpConfiguration();

            //act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(typeCnt.Get(1), cts.Token);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetRelationshipTypeShouldReturnNumber()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(
                () => ControllerFixture.MockRepository<RelationshipType>(relationshipTypeList
                    ));

            ApiRelationshipTypeController typeCntr = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            typeCntr.Request = new HttpRequestMessage();
            typeCntr.Configuration = new HttpConfiguration();

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(typeCntr.Get(1), cts.Token);

            List<RelationshipTypeModel> resultType;
            result.TryGetContentValue<List<RelationshipTypeModel>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 3);
        }

        [Fact]
        public void GetRelationshipTypeShouldOnlyDefaultNumbers()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(
                () => ControllerFixture.MockRepository<RelationshipType>(relationshipTypeList
                    ));
            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);
            ApiRelationshipTypeController typeCntr = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            typeCntr.Request = new HttpRequestMessage();
            typeCntr.Configuration = config;

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(typeCntr.Get(-1), cts.Token);

            List<RelationshipTypeModel> resultType;
            result.TryGetContentValue<List<RelationshipTypeModel>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 2);
        }

        [Fact]
        public void ShouldInsertRelationshipType()
        {
            //Arrange
            List<RelationshipType> typeResult = new List<RelationshipType>();
            var relationshipType = new RelationshipTypeModel()
            {
                RelationshipTypeId = 1,
                RelationshipTypeName = "Home",
                BookId = 1
            };
            var cbRelationshipType = new RelationshipType()
            {
                RelationshipTypeId = 1,
                Relationship_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<RelationshipType>> mockRepo = ControllerFixture.MockRepository<RelationshipType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<RelationshipType>())).Callback<RelationshipType>(
                c =>
                {
                    Mapper.CreateMap<RelationshipType, RelationshipTypeModel>().ForMember(at => at.RelationshipTypeName, cb => cb.MapFrom(m => m.Relationship_TypeName));

                    typeResult.Add(Mapper.Map<RelationshipType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ApiRelationshipTypeController relationshipController = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);

            //Act
            relationshipController.Post(relationshipType);

            //Assert
            Assert.NotEmpty(typeResult);
        }

        [Fact]
        public void ShouldInsertNumberAndReturnCreatedAndLocation()
        {
            //Arrange
            bool saveCalled = false;
            List<RelationshipType> typeResult = new List<RelationshipType>();

            var relationshipType = new RelationshipTypeModel()
            {
                RelationshipTypeId = 1,
                RelationshipTypeName = "Home",
                BookId = 1
            };

            var cbRelationshipType = new RelationshipType()
            {
                RelationshipTypeId = 1,
                Relationship_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<RelationshipType>> mockRepo = ControllerFixture.MockRepository<RelationshipType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<RelationshipType>())).Callback<RelationshipType>(
                c =>
                {
                    Mapper.CreateMap<RelationshipType, RelationshipTypeModel>().ForMember(at => at.RelationshipTypeName, cb => cb.MapFrom(m => m.Relationship_TypeName));

                    typeResult.Add(Mapper.Map<RelationshipType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(() =>
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

            ApiRelationshipTypeController typeController = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            typeController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            typeController.Configuration = config;
            typeController.Url = urlHelperMock.Object;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(typeController.Post(relationshipType), cts.Token);

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
            List<RelationshipType> typeResult = new List<RelationshipType>();

            var cbType = new RelationshipTypeModel()
            {
                RelationshipTypeId = 3,
                RelationshipTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<RelationshipType>> mockRepo = ControllerFixture.MockRepositoryNeedList<RelationshipType>(relationshipTypeList);

            mockRepo.Setup(s => s.Update(It.IsAny<RelationshipType>(), It.IsAny<RelationshipType>())).Callback<RelationshipType, RelationshipType>(
                (a, c) =>
                {
                    RelationshipType upGroup = relationshipTypeList.Where(cb => cb.RelationshipTypeId == c.RelationshipTypeId).SingleOrDefault();
                    upGroup.Relationship_TypeName = c.Relationship_TypeName;
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiRelationshipTypeController typeController = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            typeController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            typeController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(typeController.Put(cbType), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.True(relationshipTypeList.Where(cb => cb.RelationshipTypeId == 3).SingleOrDefault().Relationship_TypeName == "Updated");
        }

        [Fact]
        public void RelationshipTypeUpdateShouldReturnNotFound()
        {
            //Arrange
            var cbType = new RelationshipTypeModel()
            {
                RelationshipTypeId = -3,
                RelationshipTypeName = "Updated",
                BookId = -1
            };

            Mock<IContactBookDbRepository<RelationshipType>> mockRepo = ControllerFixture.MockRepositoryNeedList<RelationshipType>(relationshipTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiRelationshipTypeController typeController = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            typeController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            typeController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(typeController.Put(cbType), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void ShouldDeleteRelationshipTypeAndOked()
        {
            //Arrange
            bool saveCalled = false;
            List<RelationshipType> typeResult = new List<RelationshipType>();

            var groupType = new RelationshipTypeModel()
            {
                RelationshipTypeId = 3,
                RelationshipTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<RelationshipType>> mockRepo = ControllerFixture.MockRepositoryNeedList<RelationshipType>(relationshipTypeList);

            mockRepo.Setup(s => s.Delete(It.IsAny<RelationshipType>())).Callback<RelationshipType>(
                c =>
                {
                    RelationshipType delAddr = relationshipTypeList.Where(cb => cb.RelationshipTypeId == c.RelationshipTypeId).SingleOrDefault();
                    relationshipTypeList.Remove(delAddr);
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiRelationshipTypeController typeController = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            typeController.Request = new HttpRequestMessage();
            typeController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(typeController.Delete(groupType.BookId.Value, groupType.RelationshipTypeId), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.Null(relationshipTypeList.Where(cb => cb.RelationshipTypeId == 3).SingleOrDefault());
        }

        [Fact]
        public void RelationshipTypeDeleteReturnNotFound()
        {
            //Arrange
            List<RelationshipType> typeResult = new List<RelationshipType>();

            var cbType = new RelationshipTypeModel()
            {
                RelationshipTypeId = 3,
                RelationshipTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<RelationshipType>> mockRepo = ControllerFixture.MockRepositoryNeedList<RelationshipType>(relationshipTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<RelationshipType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiRelationshipTypeController numberController = new ApiRelationshipTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(2, 1), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }
    }
}