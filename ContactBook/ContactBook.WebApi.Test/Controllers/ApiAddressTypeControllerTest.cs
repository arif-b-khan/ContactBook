using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using ContactBook.WebApi.Controllers;
using ContactBook.WebApi.Test.Fixtures;
using Moq;
using Xunit;

namespace ContactBook.WebApi.Test.Controllers
{
    public class ApiAddressTypeControllerTest : IClassFixture<ControllerTestFixtures>
    {
        CancellationTokenSource cts;
        List<CB_AddressType> addressTypeList = null;
        
        public ApiAddressTypeControllerTest(ControllerTestFixtures fixture)
        {
            ControllerFixture = fixture;
            cts = new CancellationTokenSource(10000);
            addressTypeList = new List<CB_AddressType>()
            {
                new CB_AddressType(){ AddressTypeId = 1, Address_TypeName="Home", BookId=null},
                new CB_AddressType(){ AddressTypeId = 2, Address_TypeName="Office", BookId=null},
                new CB_AddressType(){ AddressTypeId = 3, Address_TypeName="abc", BookId=1}
            };
        }

        public ControllerTestFixtures ControllerFixture { get; private set; }

        [Fact]
        public void GetAddressTypeShouldReturnNotFound()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(
                () => ControllerFixture.MockRepository<CB_AddressType>(null));

            ApiAddressTypeController addressTypeCntr = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            addressTypeCntr.Request = new HttpRequestMessage();
            addressTypeCntr.Configuration = new HttpConfiguration();

            //act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(addressTypeCntr.Get(1), cts.Token);

           //Assert
            Assert.True(result.StatusCode == HttpStatusCode.NotFound);

        }

        [Fact]
        public void GetAddressTypeShouldReturnAddress()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(
                () => ControllerFixture.MockRepository<CB_AddressType>(addressTypeList
                    ));

            ApiAddressTypeController addressTypeCntr = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            addressTypeCntr.Request = new HttpRequestMessage();
            addressTypeCntr.Configuration = new HttpConfiguration();

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(addressTypeCntr.Get(1), cts.Token);
           
            List<AddressType> resultType;
            result.TryGetContentValue<List<AddressType>>(out resultType);
           
            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 3);
        
        }

        [Fact]
        public void GetAddressTypeShouldOnlyDefaultAddresses()
        {
            //Arrange
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(
                () => ControllerFixture.MockRepository<CB_AddressType>(addressTypeList
                    ));
            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);
            ApiAddressTypeController addressTypeCntr = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            addressTypeCntr.Request = new HttpRequestMessage();
            addressTypeCntr.Configuration = config;

            //Act
            HttpResponseMessage result = ControllerFixture.GetResponseMessage(addressTypeCntr.Get(-1), cts.Token);

            List<AddressType> resultType;
            result.TryGetContentValue<List<AddressType>>(out resultType);

            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(resultType);

            Assert.True(resultType.Count == 2);

        }

        [Fact]
        public void ShouldInsertAddressType()
        {
            //Arrange
            List<AddressType> addressTypeResult = new List<AddressType>();
            var addressType = new AddressType()
            {
                AddressTypeId = 1,
                AddressTypeName = "Home",
                BookId = 1
            };
            var cbaddressType = new CB_AddressType()
            {
                AddressTypeId = 1,
                Address_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_AddressType>> mockRepo = ControllerFixture.MockRepository<CB_AddressType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<CB_AddressType>())).Callback<CB_AddressType>(
                c =>
                {
                    Mapper.CreateMap<CB_AddressType, AddressType>().ForMember(at => at.AddressTypeName, cb => cb.MapFrom(m => m.Address_TypeName));

                    addressTypeResult.Add(Mapper.Map<AddressType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ApiAddressTypeController addressController = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);

            //Act
            addressController.InsertAddressTypes(addressType);

            //Assert
            Assert.NotEmpty(addressTypeResult);
        }
        
        [Fact(Skip="I'll write modelbinding test later")]
        public void AddressTypeModelBindingTest()
        {
            //var formCollection = new NameValueCollection { {AddressTypeId=1, AddressTypeName="Home", BookId=1}};
            //var valueProvider = new NameValueCollectionValueProvider
        }

        [Fact(Skip="Skip this test for while")]
        public void ShouldInsertBadRequest()
        {
            //Arrange
            List<AddressType> addressTypeResult = new List<AddressType>();
            
            var addressType = new AddressType()
            {
                AddressTypeId = 1,
                AddressTypeName = "Home",
                BookId = 1
            };

            var cbaddressType = new CB_AddressType()
            {
                AddressTypeId = 1,
                Address_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_AddressType>> mockRepo = ControllerFixture.MockRepository<CB_AddressType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<CB_AddressType>())).Callback<CB_AddressType>(
                c =>
                {
                    Mapper.CreateMap<CB_AddressType, AddressType>().ForMember(at => at.AddressTypeName, cb => cb.MapFrom(m => m.Address_TypeName));

                    addressTypeResult.Add(Mapper.Map<AddressType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ApiAddressTypeController addressController = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);

            //Act
            addressController.InsertAddressTypes(addressType);

            //Assert
            Assert.NotEmpty(addressTypeResult);
        }

        [Fact]
        public void ShouldInsertAddressAndReturnCreatedAndLocation()
        {
            //Arrange
            bool saveCalled = false;
            List<AddressType> addressTypeResult = new List<AddressType>();
            
            var addressType = new AddressType()
            {
                AddressTypeId = 1,
                AddressTypeName = "Home",
                BookId = 1
            };
            
            var cbaddressType = new CB_AddressType()
            {
                AddressTypeId = 1,
                Address_TypeName = "Home",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_AddressType>> mockRepo = ControllerFixture.MockRepository<CB_AddressType>();
            mockRepo.Setup(s => s.Insert(It.IsAny<CB_AddressType>())).Callback<CB_AddressType>(
                c =>
                {
                    Mapper.CreateMap<CB_AddressType, AddressType>().ForMember(at => at.AddressTypeName, cb => cb.MapFrom(m => m.Address_TypeName));

                    addressTypeResult.Add(Mapper.Map<AddressType>(c));
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(() =>
            {
                return mockRepo.Object;
            });
            
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });
            
            Mock<UrlHelper> urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost");
            urlHelperMock.Setup(u => u.Link(It.IsAny<string>(),It.IsAny<IDictionary<string, object>>())).Returns("http://localhost");

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiAddressTypeController addressController = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            addressController.Request = new HttpRequestMessage() {RequestUri=new Uri("http://localhost/api/testcontroller/get") };
            addressController.Configuration = config;
            addressController.Url = urlHelperMock.Object;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(addressController.InsertAddressTypes(addressType), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(resMsg.Headers.Location);
        }

        [Fact]
        public void ShouldUpdateAddressOk()
        {
            //Arrange
            bool saveCalled = false;
            List<AddressType> addressTypeResult = new List<AddressType>();
            
            var addressType = new AddressType()
            {
                AddressTypeId = 3,
                AddressTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_AddressType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_AddressType>(addressTypeList);

            mockRepo.Setup(s => s.Update(It.IsAny<CB_AddressType>())).Callback<CB_AddressType>(
                c =>
                {
                   CB_AddressType upAddr = addressTypeList.Where(cb => cb.AddressTypeId == c.AddressTypeId).SingleOrDefault();
                   upAddr.Address_TypeName = c.Address_TypeName;
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiAddressTypeController addressController = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            addressController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            addressController.Configuration = config;
            
            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(addressController.Put(addressType), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.True(addressTypeList.Where(cb => cb.AddressTypeId == 3).SingleOrDefault().Address_TypeName == "Updated");
        }

        [Fact]
        public void AddressTypeUpdateShouldReturnNotFound()
        {
            //Arrange
            var addressType = new AddressType()
            {
                AddressTypeId = -3,
                AddressTypeName = "Updated",
                BookId = -1
            };

            Mock<IContactBookDbRepository<CB_AddressType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_AddressType>(addressTypeList);
            
            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiAddressTypeController addressController = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            addressController.Request = new HttpRequestMessage() { RequestUri = new Uri("http://localhost/api/testcontroller/get") };
            addressController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(addressController.Put(addressType), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public void ShouldDeleteAddressTypeAndOked()
        {
            //Arrange
            bool saveCalled = false;
            List<AddressType> addressTypeResult = new List<AddressType>();

            var addressType = new AddressType()
            {
                AddressTypeId = 3,
                AddressTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_AddressType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_AddressType>(addressTypeList);

            mockRepo.Setup(s => s.Delete(It.IsAny<CB_AddressType>())).Callback<CB_AddressType>(
                c =>
                {
                    CB_AddressType delAddr = addressTypeList.Where(cb => cb.AddressTypeId == c.AddressTypeId).SingleOrDefault();
                    addressTypeList.Remove(delAddr);
                });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.Save()).Callback(() =>
            {
                saveCalled = true;
            });

            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiAddressTypeController addressController = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            addressController.Request = new HttpRequestMessage();
            addressController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(addressController.Delete(addressType.AddressTypeId, addressType.BookId.Value), cts.Token);

            //Assert
            Assert.True(saveCalled);
            Assert.True(resMsg.StatusCode == HttpStatusCode.OK);
            Assert.Null(addressTypeList.Where(cb => cb.AddressTypeId == 3).SingleOrDefault());
        }

        [Fact]
        public void AddressTypeDeleteReturnNotFound()
        {
            //Arrange
            bool saveCalled = false;
            List<AddressType> addressTypeResult = new List<AddressType>();

            var addressType = new AddressType()
            {
                AddressTypeId = 3,
                AddressTypeName = "Updated",
                BookId = 1
            };

            Mock<IContactBookDbRepository<CB_AddressType>> mockRepo = ControllerFixture.MockRepositoryNeedList<CB_AddressType>(addressTypeList);

            ControllerFixture.MockUnitOfWork.Setup(rp => rp.GetEntityByType<CB_AddressType>()).Returns(() =>
            {
                return mockRepo.Object;
            });

            
            var config = new HttpConfiguration();
            ControllerFixture.RouteConfig(config);

            ApiAddressTypeController addressController = new ApiAddressTypeController(ControllerFixture.MockUnitOfWork.Object, ControllerFixture.MockUnitOfWork.Object);
            addressController.Request = new HttpRequestMessage();
            addressController.Configuration = config;

            //Act
            HttpResponseMessage resMsg = ControllerFixture.GetResponseMessage(addressController.Delete(2, 1), cts.Token);

            //Assert
            Assert.True(resMsg.StatusCode == HttpStatusCode.NotFound);
        }

    }
}
