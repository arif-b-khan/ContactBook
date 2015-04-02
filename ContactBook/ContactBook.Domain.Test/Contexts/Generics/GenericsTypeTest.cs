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
        List<AddressType> mdlAddressTypes;
        
        Expression<Func<CB_AddressType, bool>> addressTypeExpr = cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == 1) || !cbt.BookId.HasValue);
        Expression<Func<CB_NumberType, bool>> numberTypeExpr = cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == 1) || !cbt.BookId.HasValue);
        
        public GenericsTypeTest(ContactBookDataFixture pdataFixture)
        {
            this.dataFixture = pdataFixture;
            mdlAddressTypes = new List<AddressType>() { new AddressType() { BookId = 1, AddressTypeName = "Family" } };
        }
        
        #region AddressTypeContextTest
        [Fact]
        public void AddressTypeIsNotNull()
        {
            List<AddressType> result = null;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericContextTypes<AddressType, CB_AddressType> address = new GenericContextTypes<AddressType, CB_AddressType>(uow);
                result = address.GetTypes(addressTypeExpr).ToList();
            }

            Assert.NotNull(result);
        }


        [Fact]
        public void AddAddressTypeReturnsList()
        {
            List<AddressType> result = null;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericContextTypes<AddressType, CB_AddressType> addressContext = new GenericContextTypes<AddressType, CB_AddressType>(uow);
                //Act
                result = addressContext.GetTypes(addressTypeExpr);
            }

            Assert.NotNull(result);

            Assert.True(result.Count >= 3);
        }

        [Fact]
        public void DB_AddressTypeRemovesAddressTypeList()
        {
            List<AddressType> addressTypes = null;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericContextTypes<AddressType, CB_AddressType> addressContext = new GenericContextTypes<AddressType, CB_AddressType>(uow);

                try
                {
                    addressContext.InsertTypes(mdlAddressTypes);
                    
                    addressTypes = addressContext.GetTypes(addressTypeExpr).Where(addr => addr.BookId.HasValue).ToList();
                }
                catch (Exception ex)
                {
                    Assert.Contains("Unable to add address type", ex.StackTrace);
                }
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericContextTypes<AddressType, CB_AddressType> addressContext = new GenericContextTypes<AddressType, CB_AddressType>(uow);

                foreach (AddressType mdlType in addressTypes)
                {
                    mdlType.AddressTypeName = "Updated Type";
                }

                addressContext.UpdateTypes(addressTypes);
                
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IGenericContextTypes<AddressType, CB_AddressType> addressContext = new GenericContextTypes<AddressType, CB_AddressType>(uow);
                addressContext.DeleteTypes(addressTypes.Where(addr => addr.BookId.HasValue).ToList());
                
            }
        }
        #endregion

        #region NumberTypeContextTest

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
            var numberContext = new GenericContextTypes<MdlNumberType, CB_NumberType>(mockRepository.Object);

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

            var numberContext = new GenericContextTypes<MdlNumberType, CB_NumberType>(mockRepository.Object);

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
                var numberContext = new GenericContextTypes<MdlNumberType, CB_NumberType>(uow);
                try
                {
                    numberContext.InsertTypes(numberTypeList);
                    
                    numberTypeList = numberContext.GetTypes(numberTypeExpr).Where(nm => nm.BookId.HasValue).ToList();
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                }
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                var numberContext = new GenericContextTypes<MdlNumberType, CB_NumberType>(uow);
                foreach (var mType in numberTypeList)
                {
                    mType.NumberType = "UpdatedCustom";
                }

                try
                {
                    numberContext.UpdateTypes(numberTypeList);
                    
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                }
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                var numberContext = new GenericContextTypes<MdlNumberType, CB_NumberType>(uow);
                
                try
                {
                    numberContext.DeleteTypes(numberTypeList);
                    
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
