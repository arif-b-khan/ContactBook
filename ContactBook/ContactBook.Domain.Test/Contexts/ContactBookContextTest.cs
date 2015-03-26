using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContactBook.Domain.Contexts;
using ContactBook.Domain.Models;
using ContactBook.Db.Data;
using Xunit;
using Moq;

using ContactBook.Domain.Test.Fixtures;
using ContactBook.Db.Repositories;

namespace ContactBook.Domain.Test.Contexts
{
    public class ContactBookContextTest : IDisposable, IClassFixture<ContactBookDataFixture>
    {
        bool disposed = false;
        MdlContactBook modelContact;
        ContactBookDataFixture contactFixture;

        public ContactBookContextTest(ContactBookDataFixture fixture)
        {
            modelContact = new MdlContactBook() { BookName = "1", Enabled = true, AspNetUserId = "User1" };
            this.contactFixture = fixture;
        }

        [Fact]
        public void AddContactBookTest()
        {
            //Arrange
            var context = new ContactBookContext("ContactBookEdmContainerTest");

            ////act
            try
            {
                context.AddContactBook(modelContact);
            }
            catch (Exception ex)
            {
                Assert.Contains("Update failed", ex.StackTrace);
            }
        }

        [Fact]
        public void GetContactBookTest()
        {
            //Arrange
            var context = new ContactBookContext("ContactBookEdmContainerTest");

            //act
            context.AddContactBook(modelContact);
            var contact = context.GetContactBook(modelContact.AspNetUserId);

            //Assert
            Assert.NotNull(contact);
        }

        [Fact]
        public void CreateContactBookTest()
        {
            //arrange
            var userContextMoq = new Mock<IUserInfoContext>();
            userContextMoq.Setup(u => u.GetUserInfo("")).Returns(new AspNetUser() {
            UserName = "UserName",
            Id = modelContact.AspNetUserId
            });
            
            //act
            IContactBookContext contactBook = new ContactBookContext(contactFixture.Catalog);
            contactBook.UserInfoContext = userContextMoq.Object;

            //assert
            try
            {
                contactBook.CreateContactBook("");
            }
            catch(Exception ex)
            {
                Assert.NotNull(ex);
            }

        }
        public void Dispose()
        {
            if (!disposed)
            {
                contactFixture.DeleteContactBookModel(modelContact);
                disposed = true;
            }
        }
    }
}
