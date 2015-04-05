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
    public class ApiGroupTypeControllerTest : IClassFixture<ControllerTestFixtures>
    {
        private CancellationTokenSource cts;
        private List<CB_GroupType> groupTypeList = null;

        public ApiGroupTypeControllerTest(ControllerTestFixtures fixture)
        {
            ControllerFixture = fixture;
            cts = new CancellationTokenSource(10000);
            groupTypeList = new List<CB_GroupType>()
            {
                new CB_GroupType(){ GroupId = 1, Group_TypeName="Home", BookId=null},
                new CB_GroupType(){ GroupId = 2, Group_TypeName="Office", BookId=null},
                new CB_GroupType(){ GroupId = 3, Group_TypeName="abc", BookId=1}
            };
        }

        public ControllerTestFixtures ControllerFixture { get; private set; }

        [Fact]
        public void GetGroupTypeShouldReturnNotFound()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(() => ControllerFixture.MockRepository<CB_GroupType>(null));

            ApiGroupTypeController groupTypeCnt = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            groupTypeCnt.Request = new HttpRequestMessage();
            groupTypeCnt.Configuration = new HttpConfiguration();

            //act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(groupTypeCnt.Get(1), cts.Token);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetGroupTypeShouldReturnNumber()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(
                () => ControllerFixture.MockRepository<CB_GroupType>(groupTypeList
                    ));

            ApiGroupTypeController groupTypeCntr = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            groupTypeCntr.Request = new HttpRequestMessage();
            groupTypeCntr.Configuration = new HttpConfiguration();

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(groupTypeCntr.Get(1), cts.Token);

            List<GroupType> resultType;
            result.TryGetContentValue<List<GroupType>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 3);
        }

        [Fact]
        public void GetGroupTypeShouldOnlyDefaultNumbers()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(
                () => ControllerFixture.MockRepository<CB_GroupType>(groupTypeList
                    ));
            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);
            ApiGroupTypeController groupTypeCntr = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            groupTypeCntr.Request = new HttpRequestMessage();
            groupTypeCntr.Configuration = config;

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(groupTypeCntr.Get(-1), cts.Token);

            List<GroupType> resultType;
            result.TryGetContentValue<List<GroupType>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 2);
        }

        [Fact]
        public void ShouldInsertGroupType()
        {
            //Arrange
            List<GroupType> groupTypeResult = new List<GroupType>();
            var groupType = new GroupType()
            {
                GroupId = 1,
                GroupTypeName = "Home",
                BookId = 1
            };
            var cbGroupType = new CB_GroupType()
            {
                GroupId = 1,
                Group_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_GroupType>> mockRepo = ControllerFixture.MockRepository<CB_GroupType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<CB_GroupType>())).Callback<CB_GroupType>(
                c =>
                {
                    Mapper.CreateMap<CB_GroupType, GroupType>().ForMember(at => at.GroupTypeName, cb => cb.MapFrom(m => m.Group_TypeName));

                    groupTypeResult.Add(Mapper.Map<GroupType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ApiGroupTypeController numberController = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);

            //Act
            numberController.Post(groupType);

            //Assert
            Assert.NotEmpty(groupTypeResult);
        }

        [Fact]
        public void ShouldInsertNumberAndReturnCreatedAndLocation()
        {
            //Arrange
            bool saveCalled = false;
            List<GroupType> groupTypeResult = new List<GroupType>();

            var groupType = new GroupType()
            {
                GroupId = 1,
                GroupTypeName = "Home",
                BookId = 1
            };

            var cbGroupType = new CB_GroupType()
            {
                GroupId = 1,
                Group_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_GroupType>> mockRepo = ControllerFixture.MockRepository<CB_GroupType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<CB_GroupType>())).Callback<CB_GroupType>(
                c =>
                {
                    Mapper.CreateMap<CB_GroupType, GroupType>().ForMember(at => at.GroupTypeName, cb => cb.MapFrom(m => m.Group_TypeName));

                    groupTypeResult.Add(Mapper.Map<GroupType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(() =>
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

            ApiGroupTypeController numberController = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;
            numberController.Url = urlHelperMock.Object;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Post(groupType), cts.Token);

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
            List<GroupType> groupTypeResult = new List<GroupType>();

            var imType = new GroupType()
            {
                GroupId = 3,
                GroupTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_GroupType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_GroupType>(groupTypeList);

            mockRepo.Setup(s => s.Update(It.IsAny<CB_GroupType>())).Callback<CB_GroupType>(
                c =>
                {
                    CB_GroupType upGroup = groupTypeList.Where(cb => cb.GroupId == c.GroupId).SingleOrDefault();
                    upGroup.Group_TypeName = c.Group_TypeName;
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiGroupTypeController numberController = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Put(imType), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.True(groupTypeList.Where(cb => cb.GroupId == 3).SingleOrDefault().Group_TypeName == "Updated");
        }

        [Fact]
        public void GroupTypeUpdateShouldReturnNotFound()
        {
            //Arrange
            var imType = new GroupType()
            {
                GroupId = -3,
                GroupTypeName = "Updated",
                BookId = -1
            };

            Mock<IContactBookDbRepository<CB_GroupType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_GroupType>(groupTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiGroupTypeController numberController = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Put(imType), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void ShouldDeleteGroupTypeAndOked()
        {
            //Arrange
            bool saveCalled = false;
            List<GroupType> groupTypeResult = new List<GroupType>();

            var groupType = new GroupType()
            {
                GroupId = 3,
                GroupTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_GroupType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_GroupType>(groupTypeList);

            mockRepo.Setup(s => s.Delete(It.IsAny<CB_GroupType>())).Callback<CB_GroupType>(
                c =>
                {
                    CB_GroupType delAddr = groupTypeList.Where(cb => cb.GroupId == c.GroupId).SingleOrDefault();
                    groupTypeList.Remove(delAddr);
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiGroupTypeController numberController = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(groupType.BookId.Value, groupType.GroupId), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.Null(groupTypeList.Where(cb => cb.GroupId == 3).SingleOrDefault());
        }

        [Fact]
        public void GroupTypeDeleteReturnNotFound()
        {
            //Arrange
            bool saveCalled = false;
            List<GroupType> groupTypeResult = new List<GroupType>();

            var groupType = new GroupType()
            {
                GroupId = 3,
                GroupTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_GroupType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_GroupType>(groupTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_GroupType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiGroupTypeController numberController = new ApiGroupTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            numberController.Request = new HttpRequestMessage();
            numberController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(numberController.Delete(2, 1), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }
    }
}