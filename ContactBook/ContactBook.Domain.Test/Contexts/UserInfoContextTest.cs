using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        //[Fact(Skip="Skip this test until if figure out to work with this test")]
        [Fact]
        public void GetUserInfoTest_NotNull()
        {
            //Arrange
            var contextUow = new Mock<IContactBookRepositoryUow>();
            contextUow.Setup(uow => uow.GetEntityByType<AspNetUser>()).Returns(() => {
                return dataFixture.ContactBookDbRepository;
            });

            IUserInfoContext context = new UserInfoContext(contextUow.Object);

            //act
            AspNetUser aspNet = context.GetUserInfo("user1");
           
            Assert.NotNull(aspNet);
        }
    }
}
