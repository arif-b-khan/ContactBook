using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Contacts;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.Domain.Test.Fixtures;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContactBook.Domain.Test.Contexts.Contacts
{
    public class ContactContextTest : IClassFixture<ContactBookDataFixture>
    {
        private ContactBookDataFixture contactFixture;

        public ContactContextTest(ContactBookDataFixture fixture)
        {
            this.contactFixture = fixture;
        }

        [Fact]
        public void GetContactTestHasRecord()
        {
            Contact mcontact;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IContactContext contact = new ContactContext(uow);
                mcontact = contact.GetContact(1);
            }

            Assert.NotNull(mcontact);
        }

        [Fact]
        public void InsertContactTest()
        {
            Contact inContact = new Contact()
            {
                BookId = 1,
                Firstname = "Tarik",
                Numbers = new List<Number>() { new Number() { ContactNumber = "8879856423", NumberTypeId = 1, ContactId = 0, NumberId=0 } }
            };
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IContactContext contact = new ContactContext(uow);
                contact.InsertContact(inContact);
            }
        }

        [Fact]
        public void DeleteContactTest()
        {
            Contact contact = null;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                contact = new ContactContext(uow).GetContact(2);
            }
            contact.Numbers.Add(new Number() { ContactId = 2, ContactNumber = "12341234", NumberTypeId = 2 });
            if (contact != null)
            {
                using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
                {
                    IContactContext conDelete = new ContactContext(uow);
                    conDelete.DeleteContact(contact);
                }
            }
        }


        [Fact]
        public void UpdateContactTest()
        {
            Contact contact = null;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                contact = new ContactContext(uow).GetContact(2);
            }
            var number = contact.Numbers.First();
            contact.Numbers.Remove(number);
            if (contact != null)
            {
                using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
                {
                    IContactContext conUpdate = new ContactContext(uow);
                    conUpdate.UpdateContact(contact);
                }
            }
        }

    }
}