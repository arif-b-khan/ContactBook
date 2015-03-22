using ContactBook.Db;
using ContactBook.Db.Implementation;
using ContactBook.Db.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactBook.Domain.Test
{
    public class UnitOfWorkTest
    {
        [Fact]
        public void AddressAdd()
        {
            ////arrange
            ////UnitOfWork work = new UnitOfWork();
            //IUnityContainer container = new UnityContainer()
            //    .RegisterType(typeof(ContactBookDbRepository<CB_Address>), typeof(CB_Address));

            ////act
            ////IContactBookRepository<IEntity> result = (IContactBookRepository<IEntity>)container.Resolve<ContactBookDbRepository<CB_Address>>();

            ////assert
            //Assert.NotNull(result);
        }
    }
}
