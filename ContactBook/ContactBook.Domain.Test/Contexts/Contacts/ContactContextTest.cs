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

            IContactContext contact = new ContactContext();
            mcontact = contact.GetContact(1);

            Assert.NotNull(mcontact);
        }

        [Fact(Skip = "skip this until test completes")]
        public void InsertContactTest()
        {
            Contact inContact = new Contact()
            {
                BookId = 1,
                Firstname = "Tarik",
                Numbers = new List<Number>() { new Number() { ContactNumber = "8879856423", NumberTypeId = 1, ContactId = 0, NumberId = 0 } }
            };

            IContactContext contact = new ContactContext();
            contact.InsertContact(inContact);
        }

        [Fact(Skip="skip this until test completes")]
        public void DeleteContactTest()
        {
            Contact contact = new ContactContext().GetContact(2);

            contact.Numbers.Add(new Number() { ContactId = 2, ContactNumber = "12341234", NumberTypeId = 2 });
            if (contact != null)
            {
                IContactContext conDelete = new ContactContext();
                conDelete.DeleteContact(contact);
            }
        }


        [Fact(Skip = "skip this until test completes")]
        public void UpdateContactTest()
        {
            Contact contact = new ContactContext().GetContact(2);

            var number = contact.Numbers.First();
            contact.Numbers.Remove(number);
            if (contact != null)
            {
                IContactContext conUpdate = new ContactContext();
                conUpdate.UpdateContact(contact);
            }
        }

    }
}