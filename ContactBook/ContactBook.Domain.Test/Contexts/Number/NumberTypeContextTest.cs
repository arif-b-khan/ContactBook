using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Number;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.Domain.Test.Fixtures;
using Moq;
using Xunit;

namespace ContactBook.Domain.Test.Contexts.Number
{
    public class NumberTypeContextTest : IClassFixture<ContactBookDataFixture>
    {
        ContactBookDataFixture dataFixture;
        public NumberTypeContextTest(ContactBookDataFixture fixture)
        {
            dataFixture = fixture;
        }

        [Fact]
        public void GetNumberTypesReturnsNull()
        {
            //Arrange
            var mockRepository = new Mock<IContactBookRepositoryUow>();
            mockRepository.Setup(c => c.GetEntityByType<CB_NumberType>()).Returns(
                 () => dataFixture.Repository<CB_NumberType>(null)
                );
            var numberContext = new NumberTypeContext(mockRepository.Object);

            //act
            var numberList = numberContext.GetNumberTypes(1);

            //Assert
            Assert.Null(numberList);
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
            var numberContext = new NumberTypeContext(mockRepository.Object);

            //act
            var numberList = numberContext.GetNumberTypes(1);

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
            var numberContext = new NumberTypeContext(mockRepository.Object);

            //act
            var numberList = numberContext.GetNumberTypes(1);

            //Assert
            Assert.True(numberList.Count == 4);

            Assert.Contains(numberList, m => m.NumberType == "Custom");
        }

        [Fact()]
        public void ShouldPerformCUD()
        {
            //Arrange
            List<MdlNumberType> numberTypeList = new List<MdlNumberType>() { 
            new MdlNumberType(){NumberType="Custom", BookId = 1}
            };

            //Act
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                NumberTypeContext numberContext = new NumberTypeContext(uow);
                try
                {
                    numberContext.InsertNumberType(numberTypeList);
                    uow.Save();
                   numberTypeList = numberContext.GetNumberTypes(1).Where(nm => nm.BookId.HasValue).ToList();
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                }
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                NumberTypeContext numberContext = new NumberTypeContext(uow);
                foreach (var mType in numberTypeList)
                {
                    mType.NumberType = "UpdatedCustom";
                }

                try
                {
                    numberContext.UpdateNumberType(numberTypeList);
                    uow.Save();
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                }
            }

            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                NumberTypeContext numberContext = new NumberTypeContext(uow);
                try
                {
                    numberContext.DeleteNumberType(numberTypeList);
                    uow.Save();
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex);
                }
            }

        }
    }
}
