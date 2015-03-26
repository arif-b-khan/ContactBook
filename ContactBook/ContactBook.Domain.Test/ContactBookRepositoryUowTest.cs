using ContactBook.Db;
using ContactBook.Db.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ContactBook.Db.Data;
using ContactBook.Domain.Test.Fixtures;

namespace ContactBook.Domain.Test
{
    public class ContactBookRepositoryUowTest : IClassFixture<ContactBookDataFixture>, IDisposable
    {
        bool disposed = false;
        ContactBookDataFixture dataFixture;

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
