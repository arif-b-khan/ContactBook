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

namespace ContactBook.Domain.Test
{
    public class ContactBookRepositoryUowTest
    {
        [Fact]
        public void GetEntityByType_TestForNotNull()
        {
            //arrange
            ContactBookRepositoryUow work = new ContactBookRepositoryUow(new ContactBookEdmContainer());
            //IUnityContainer container = new UnityContainer()

            //act
            var address = work.GetEntityByType<CB_Address>();

            //assert
            Assert.NotNull(address);
        }
    }
}
