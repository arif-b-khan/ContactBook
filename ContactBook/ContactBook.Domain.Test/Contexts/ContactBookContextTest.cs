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
using ContactBook.Domain.IoC;

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
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                //Arrange
                var context = new ContactBookContext(uow);

                ////act
                try
                {
                    context.AddContactBook(modelContact);
                    uow.Save();
                }
                catch (Exception ex)
                {
                    Assert.Contains("Update failed", ex.StackTrace);
                }
            }
        }

        [Fact]
        public void GetContactBookTest()
        {
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                //Arrange
                var context = new ContactBookContext(uow);

                //act
                context.AddContactBook(modelContact);
                uow.Save();
                var contact = context.GetContactBook(modelContact.AspNetUserId);

                //Assert
                Assert.NotNull(contact);
            }
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

            var unitOfWorkMoq = new Mock<IContactBookRepositoryUow>();
            unitOfWorkMoq.Setup(un => un.GetEntityByType<CB_ContactBook>()).Returns(() =>
            {
                return contactFixture.Repository<CB_ContactBook>(new List<CB_ContactBook>() { 
                new CB_ContactBook(){BookId = 1, BookName="1-axkhan", AspNetUserId="1"}
                });
            });
            unitOfWorkMoq.Setup(un => un.Save());
            //act
            IContactBookContext contactBook = new ContactBookContext(unitOfWorkMoq.Object, userContextMoq.Object);

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
