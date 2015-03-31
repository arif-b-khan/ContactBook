using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Generics;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.Domain.Test.Fixtures;
using Moq;
using Xunit;

namespace ContactBook.Domain.Test.Contexts.Generics
{
    public class GenericsTypeTest : IDisposable, IClassFixture<ContactBookDataFixture>
    {
        public ContactBookDataFixture dataFixture;
        List<MdlAddressType> mdlAddressTypes;
        
        Expression<Func<CB_AddressType, bool>> addressTypeExpr = cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == 1) || !cbt.BookId.HasValue);
        Expression<Func<CB_NumberType, bool>> numberTypeExpr = cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == 1) || !cbt.BookId.HasValue);
        
        public GenericsTypeTest(ContactBookDataFixture pdataFixture)
        {
            this.dataFixture = pdataFixture;
            mdlAddressTypes = new List<MdlAddressType>() { new MdlAddressType() { BookId = 1, Address_TypeName = "Family" } };
        }
        
        #region AddressTypeContextTest
        [Fact]
        public void AddressTypeIsNotNull()
        {
            List<MdlAddressType> result = null;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericTypes<MdlAddressType, CB_AddressType> address = new GenericTypes<MdlAddressType, CB_AddressType>(uow);
                result = address.GetTypes(addressTypeExpr).ToList();
            }

            Assert.NotNull(result);
        }


        [Fact]
        public void AddAddressTypeReturnsList()
        {
            List<MdlAddressType> result = null;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericTypes<MdlAddressType, CB_AddressType> addressContext = new GenericTypes<MdlAddressType, CB_AddressType>(uow);
                //Act
                result = addressContext.GetTypes(addressTypeExpr);
            }

            Assert.NotNull(result);

            Assert.True(result.Count >= 3);
        }

        [Fact]
        public void DB_AddressTypeRemovesAddressTypeList()
        {
            List<MdlAddressType> addressTypes = null;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericTypes<MdlAddressType, CB_AddressType> addressContext = new GenericTypes<MdlAddressType, CB_AddressType>(uow);

                try
                {
                    addressContext.InsertTypes(mdlAddressTypes);
                    uow.Save();
                    addressTypes = addressContext.GetTypes(addressTypeExpr).Where(addr => addr.BookId.HasValue).ToList();
                }
                catch (Exception ex)
                {
                    Assert.Contains("Unable to add address type", ex.StackTrace);
                }
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericTypes<MdlAddressType, CB_AddressType> addressContext = new GenericTypes<MdlAddressType, CB_AddressType>(uow);

                foreach (MdlAddressType mdlType in addressTypes)
                {
                    mdlType.Address_TypeName = "Updated Type";
                }

                addressContext.UpdateTypes(addressTypes);
                uow.Save();
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericTypes<MdlAddressType, CB_AddressType> addressContext = new GenericTypes<MdlAddressType, CB_AddressType>(uow);
                addressContext.DeleteTypes(addressTypes.Where(addr => addr.BookId.HasValue).ToList());
                uow.Save();
            }
        }
        #endregion

        #region NumberTypeContextTest

        [Fact]
        public void GetNumberTypesThrowsArgsNullException()
        {
            //Arrange
            var mockRepository = new Mock<IContactBookRepositoryUow>();
            mockRepository.Setup(c => c.GetEntityByType<CB_NumberType>()).Returns(
                 () => dataFixture.Repository<CB_NumberType>(null)
                );

            var numberContext = new GenericTypes<MdlNumberType, CB_NumberType>(mockRepository.Object);

            //act and Assert
            Assert.Throws<ArgumentNullException>(() => numberContext.GetTypes(numberTypeExpr));
        }

        [Fact]
        public void GetNumberTypesReturnsOneRecord()
        {
            //Arrange
            var mockRepository = new Mock<IContactBookRepositoryUow>();
            mockRepository.Setup(c => c.GetEntityByType<CB_NumberType>()).Returns(
                 () => dataFixture.Repository<CB_NumberType>(new List<CB_NumberType>(){new CB_NumberType() { 
                 NumberTypeId = 1, Number_TypeName="Mobile", BookId=null
                 }})
                );
            var numberContext = new GenericTypes<MdlNumberType, CB_NumberType>(mockRepository.Object);

            //act
            var numberList = numberContext.GetTypes(numberTypeExpr);

            //Assert
            Assert.Single<MdlNumberType>(numberList);
        }


        [Fact]
        public void GetNumberTypes_ReturnDefaultAndUserSpecificNumberTypes()
        {
            //Arrange
            var mockRepository = new Mock<IContactBookRepositoryUow>();
            mockRepository.Setup(c => c.GetEntityByType<CB_NumberType>()).Returns(
                 () => dataFixture.Repository<CB_NumberType>(new List<CB_NumberType>(){new CB_NumberType() { 
                 NumberTypeId = 1, Number_TypeName="Mobile", BookId=null
                 },
                 new CB_NumberType() { 
                 NumberTypeId = 2, Number_TypeName="Home", BookId=null
                 },
new CB_NumberType() { 
                 NumberTypeId = 3, Number_TypeName="Office", BookId=null
                 },
new CB_NumberType() { 
                 NumberTypeId = 4, Number_TypeName="Custom", BookId=1
                 }
                 })
                );

            var numberContext = new GenericTypes<MdlNumberType, CB_NumberType>(mockRepository.Object);

            //act
            var numberList = numberContext.GetTypes(numberTypeExpr);

            //Assert
            Assert.True(numberList.Count == 4);

            Assert.Contains(numberList, m => m.NumberType == "Custom");
        }

        [Fact()]
        public void ShouldPerformCUDOnCBNumberTypes()
        {
            //Arrange
            List<MdlNumberType> numberTypeList = new List<MdlNumberType>() { 
            new MdlNumberType(){NumberType="Custom", BookId = 1}
            };

            //Act
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                var numberContext = new GenericTypes<MdlNumberType, CB_NumberType>(uow);
                try
                {
                    numberContext.InsertTypes(numberTypeList);
                    uow.Save();
                    numberTypeList = numberContext.GetTypes(numberTypeExpr).Where(nm => nm.BookId.HasValue).ToList();
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                }
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                var numberContext = new GenericTypes<MdlNumberType, CB_NumberType>(uow);
                foreach (var mType in numberTypeList)
                {
                    mType.NumberType = "UpdatedCustom";
                }

                try
                {
                    numberContext.UpdateTypes(numberTypeList);
                    uow.Save();
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                }
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                var numberContext = new GenericTypes<MdlNumberType, CB_NumberType>(uow);
                
                try
                {
                    numberContext.DeleteTypes(numberTypeList);
                    uow.Save();
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                }
            }

        }

        #endregion

        public void Dispose()
        {

        }
    }
}
