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
        List<MdlAddressType> mdlAddressTypes;

        public AddressTypeContextTest(ContactBookDataFixture dataFixture)
        {
            this.dataFixture = dataFixture;
            mdlAddressTypes = new List<MdlAddressType>() { new MdlAddressType() { BookId = 1, Address_TypeName = "Family" } };
        }

        [Fact]
        public void AddAddressTypeReturnsList()
        {
            List<MdlAddressType> result = null;
            using (IContactBookRepositoryUow uow = new ContactBookRepositoryUow(new ContactBookEdmContainer(dataFixture.Catalog)))
            {
                IAddressTypeContext addressContext = new AddressTypeContext(uow);
                //Act
                result = addressContext.GetAddessType(1);
            }

            Assert.NotNull(result);

            Assert.True(result.Count >= 3);
        }

        [Fact]
        public void DB_AddressTypeRemovesAddressTypeList()
        {
            List<MdlAddressType> addressTypes = null;
            using (IContactBookRepositoryUow uow = new ContactBookRepositoryUow(new ContactBookEdmContainer(dataFixture.Catalog)))
            {
                IAddressTypeContext addressContext = new AddressTypeContext(uow);
                //List<MdlAddressType> addresses = addressContext.GetAddessType(7);
                try
                {
                    addressContext.AddAddressTypes(mdlAddressTypes);
                    addressTypes = addressContext.GetAddessType(1).Where(addr => addr.BookId.HasValue).ToList();
                }
                catch (Exception ex)
                {
                    Assert.Contains("Unable to add address type", ex.StackTrace);
                }
            }

            using (IContactBookRepositoryUow uow = new ContactBookRepositoryUow(new ContactBookEdmContainer(dataFixture.Catalog)))
            {
                IAddressTypeContext addressContext = new AddressTypeContext(uow);

                foreach (MdlAddressType mdlType in addressTypes)
                {
                    mdlType.Address_TypeName = "Updated Type";
                }

                addressContext.UpdateAddressTypes(addressTypes);
            }

            using (IContactBookRepositoryUow uow = new ContactBookRepositoryUow(new ContactBookEdmContainer(dataFixture.Catalog)))
            {
                IAddressTypeContext addressContext = new AddressTypeContext(uow);
                addressContext.RemoveAddressTypes(addressTypes.Where(addr => addr.BookId.HasValue).ToList());
            }
        }

        public void Dispose()
        {

        }
    }
}
