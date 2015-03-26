using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts;
using ContactBook.Domain.Test.Fixtures;
using Moq;
using Xunit;

namespace ContactBook.Domain.Test.Contexts
{
    public class UserInfoContextTest : IClassFixture<ContactBookDataFixture>
    {
        ContactBookDataFixture dataFixture;

        public UserInfoContextTest(ContactBookDataFixture dataFixture)
        {
            this.dataFixture = dataFixture;
        }

        [Fact(Skip="Skip this test until if figure out to work with this test")]
        public void GetUserInfoTest_NotNull()
        {
            //Arrange
            var contactRepo = new Mock<ContactBookDbRepository<AspNetUser>>(dataFixture.Container);
            contactRepo.Setup(rp => rp.Get(null, null, null)).Returns(new List<AspNetUser>(){
            new AspNetUser(){Id = "1", UserName="user1"}
            });

            var contextUow = new Mock<IContactBookRepositoryUow>();
            contextUow.Setup(uow => uow.GetEntityByType<AspNetUser>()).Returns(contactRepo.Object);

            IUserInfoContext context = new UserInfoContext(dataFixture.Catalog);
            context.UnitOfWork = contextUow.Object;

            //act
            AspNetUser aspNet = context.GetUserInfo("user1");

            //Assert
            Assert.NotNull(aspNet);
        }
    }
}
