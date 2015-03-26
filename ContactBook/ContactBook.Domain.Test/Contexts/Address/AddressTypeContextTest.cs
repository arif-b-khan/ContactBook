using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Address;
using ContactBook.Domain.Models;
using ContactBook.Domain.Test.Fixtures;
using Moq;
using Xunit;

namespace ContactBook.Domain.Test.Contexts.Address
{
    public class AddressTypeContextTest : IDisposable, IClassFixture<ContactBookDataFixture>
    {
        ContactBookDataFixture dataFixture;
        
        public AddressTypeContextTest(ContactBookDataFixture dataFixture)
        {
            this.dataFixture = dataFixture;
        }

        [Fact]
        public void AddAddressTypeThrowsNullReferenceException()
        {
            //Arrange
            IAddressTypeContext address = new AddressTypeContext(dataFixture.Catalog);
            address.UnitOfWork = null;

            //Act and Assert
            Assert.Throws<NullReferenceException>(() => address.GetAddessType(0));
        }

        [Fact]
        public void AddAddressTypeReturnsList()
        {
            //Arrange 
            IAddressTypeContext addressContext = new AddressTypeContext(dataFixture.Catalog);

            //Act
           List<MdlAddressType> result = addressContext.GetAddessType(0);

           Assert.NotNull(result);

           Assert.True(result.Count >= 3);
        }

        public void Dispose()
        {

        }
    }
}
