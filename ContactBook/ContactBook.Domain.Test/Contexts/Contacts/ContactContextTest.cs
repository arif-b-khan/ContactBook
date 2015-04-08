using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Contacts;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.Domain.Test.Fixtures;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using ContactBook.Db.Data;

namespace ContactBook.Domain.Test.Contexts.Contacts
{
    public class ContactContextTest : IClassFixture<ContactBookDataFixture>
    {
        private ContactBookDataFixture contactFixture;
        private List<CB_Contact> cbContactList;

        public ContactContextTest(ContactBookDataFixture fixture)
        {
            this.contactFixture = fixture;
            cbContactList = new List<CB_Contact>() {
            new CB_Contact(){ BookId = 1, Firstname = "Arif", Lastname="Khan", CB_Numbers = new List<CB_Number>(){ 
                new CB_Number(){NumberId=1, NumberTypeId=1, ContactId=1, Number="999999999"},
                new CB_Number(){NumberId=2, NumberTypeId=1, ContactId=1, Number="888888888"},
                new CB_Number(){NumberId=2, NumberTypeId=1, ContactId=1, Number="888888888"}
            }}
            };
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

        [Fact(Skip = "skip this until test completes")]
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

        [Fact]
        public void UpdateContactTestCBNumbersCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var numbersList = new List<CB_Number>(){ 
                new CB_Number(){NumberId=1, NumberTypeId=1, ContactId=1, Number="999999999"},
                new CB_Number(){NumberId=2, NumberTypeId=1, ContactId=1, Number="888888888"},
                new CB_Number(){NumberId=3, NumberTypeId=1, ContactId=1, Number="888888888"}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_Numbers = numbersList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", Numbers = new List<Number>(){ 
                new Number(){NumberId=2, NumberTypeId=1, ContactId=1, ContactNumber="000000000"},
                new Number(){NumberId=0, NumberTypeId=2, ContactId=1, ContactNumber="777777777"}
            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_Number>> numberRepo = contactFixture.MockRepository<CB_Number>(numbersList);
            numberRepo.Setup(nm => nm.Insert(It.IsAny<CB_Number>())).Callback<CB_Number>((cb) =>
            {
                if (cb.Number == "777777777" &&
                    cb.ContactId == 1 &&
                    cb.NumberTypeId == 2)
                {
                    insertValue = true;
                }
            });
            numberRepo.Setup(nm => nm.Delete(It.IsAny<CB_Number>())).Callback<CB_Number>((cb) =>
            {
                if (cb.Number == "999999999" &&
                    cb.ContactId == 1 &&
                    cb.NumberTypeId == 1 &&
                    cb.NumberId == 1)
                {
                    deleteValue = true;
                }
            });
            numberRepo.Setup(nm => nm.Update(It.IsAny<CB_Number>())).Callback<CB_Number>((cb) =>
            {
                if (cb.Number == "000000000" &&
                    cb.ContactId == 1 &&
                    cb.NumberTypeId == 1 &&
                    cb.NumberId == 2)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_Number>()).Returns(() => numberRepo.Object);

            //act
            IContactContext contactContextTest = new ContactContext(uow.Object, uow.Object);
            contactContextTest.UpdateContact(modelContactList.First());

            //assert
            Assert.True(insertValue);
            Assert.True(updateValue);
            Assert.True(deleteValue);

        }

        [Fact]
        public void UpdateContactTestCBEmailsCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var emailsList = new List<CB_Email>(){ 
                new CB_Email(){EmailId=1, EmailTypeId=1, ContactId=1, Email="abc1@gmail.com"},
                new CB_Email(){EmailId=2, EmailTypeId=1, ContactId=1, Email="abc2@gmail.com"},
                new CB_Email(){EmailId=3, EmailTypeId=1, ContactId=1, Email="abc3@gmail.com"}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_Emails = emailsList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", Emails = new List<Email>(){ 
                new Email(){EmailId=2, EmailTypeId=1, ContactId=1, EmailAddress="arif@gmail.com"},
                new Email(){EmailId=3, EmailTypeId=1, ContactId=1, EmailAddress="asif@gmail.com"},
                new Email(){EmailId=0, EmailTypeId=1, ContactId=1, EmailAddress="inserted@gmail.com"},

            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_Email>> emailRepo = contactFixture.MockRepository<CB_Email>(emailsList);
            emailRepo.Setup(nm => nm.Insert(It.IsAny<CB_Email>())).Callback<CB_Email>((cb) =>
            {
                if (cb.Email == "inserted@gmail.com" &&
                    cb.ContactId == 1 &&
                    cb.EmailTypeId == 1)
                {
                    insertValue = true;
                }
            });
            emailRepo.Setup(nm => nm.Delete(It.IsAny<CB_Email>())).Callback<CB_Email>((cb) =>
            {
                if (cb.Email == "abc1@gmail.com" &&
                    cb.ContactId == 1 &&
                    cb.EmailTypeId == 1 &&
                    cb.EmailId == 1)
                {
                    deleteValue = true;
                }
            });
            emailRepo.Setup(nm => nm.Update(It.IsAny<CB_Email>())).Callback<CB_Email>((cb) =>
            {
                if (cb.Email == "abc3@gmail.com" &&
                    cb.ContactId == 1 &&
                    cb.EmailTypeId == 1 &&
                    cb.EmailId == 3)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_Email>()).Returns(() => emailRepo.Object);

            //act
            IContactContext contactContextTest = new ContactContext(uow.Object, uow.Object);
            contactContextTest.UpdateContact(modelContactList.First());

            //assert
            Assert.True(insertValue);
            Assert.True(updateValue);
            Assert.True(deleteValue);

        }

        [Fact(Skip = "Skip this test for will")]
        //[Fact]
        public void UpdateContactTest()
        {

            Contact contact = new ContactContext().GetContact(2);

            if (contact.Numbers != null && contact.Numbers.Count >= 1)
            {
                foreach (var item in contact.Numbers.Where(n => n.NumberId == 3 || n.NumberId == 4))
                {
                    if (item.NumberId == 3)
                        item.ContactNumber = "5544332211";
                    else
                        item.ContactNumber = "1122334455";
                }
            }
            Number number = contact.Numbers.Last();
            contact.Numbers.Remove(number);
            contact.Numbers.Add(new Number() { NumberId = 0, ContactId = 2, ContactNumber = "111111111", NumberTypeId = 1 });

            if (contact != null)
            {
                IContactContext conUpdate = new ContactContext();
                conUpdate.UpdateContact(contact);
            }
        }

    }
}