using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Test.Fixtures;
using System;
using Xunit;

namespace ContactBook.Domain.Test
{
    public class ContactBookRepositoryUowTest : IClassFixture<ContactBookDataFixture>, IDisposable
    {
        private bool disposed = false;
        private ContactBookDataFixture dataFixture;

        public ContactBookRepositoryUowTest(ContactBookDataFixture fixture)
        {
            dataFixture = fixture;
        }

        [Fact]
        public void GetEntityByType_TestForNotNull()
        {
            //arrange
            ContactBookRepositoryUow work = dataFixture.UnitOfWork;
            //IUnityContainer container = new UnityContainer()

            //act
            var address = work.GetEntityByType<CB_Address>();

            //assert
            Assert.NotNull(address);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                dataFixture.Dispose();
                disposed = true;
            }
        }
    }
}