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
            IContactBookRepositoryUow work = dataFixture.UnitOfWork;

            //act
            var address = work.GetEntityByType<CB_Address>();

            //assert
            Assert.NotNull(address);
        }

        public virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                dataFixture.Dispose();
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                Dispose(true);
                disposed = true;
            }
        }
    }
}