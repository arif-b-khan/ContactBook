using System;
using System.Collections.Generic;
using System.Linq;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Contacts;
using ContactBook.Domain.Models;
using ContactBook.Domain.Test.Fixtures;
using Moq;
using Xunit;
using ContactBook.Domain.IoC;

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

            IContactContext contact = DependencyFactory.Resolve<IContactContext>();
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

            IContactContext contact = DependencyFactory.Resolve<IContactContext>();
            contact.InsertContact(inContact);
        }

        [Fact(Skip = "skip this until test completes")]
        public void DeleteContactTest()
        {
            Contact contact = DependencyFactory.Resolve<IContactContext>().GetContact(2);

            contact.Numbers.Add(new Number() { ContactId = 2, ContactNumber = "12341234", NumberTypeId = 2 });
            if (contact != null)
            {
                IContactContext conDelete = DependencyFactory.Resolve<IContactContext>();
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
                if (cb.Email == "arif@gmail.com" &&
                    cb.ContactId == 1 &&
                    cb.EmailTypeId == 1 &&
                    cb.EmailId == 2)
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

        [Fact]
        public void UpdateContactTestCBIMCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var imsList = new List<CB_IM>(){
                new CB_IM(){IMId=1, IMTypeId=1, ContactId=1, Username="abc1@gmail.com"},
                new CB_IM(){IMId=2, IMTypeId=1, ContactId=1, Username="abc2@gmail.com"},
                new CB_IM(){IMId=3, IMTypeId=1, ContactId=1, Username="abc3@gmail.com"}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_IMs = imsList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", IMs = new List<IM>(){
                new IM(){IMId=2, IMTypeId=1, ContactId=1, Username="arif@gmail.com"},
                new IM(){IMId=3, IMTypeId=1, ContactId=1, Username="asif@gmail.com"},
                new IM(){IMId=0, IMTypeId=1, ContactId=1, Username="inserted@gmail.com"},
            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_IM>> imRepo = contactFixture.MockRepository<CB_IM>(imsList);
            imRepo.Setup(nm => nm.Insert(It.IsAny<CB_IM>())).Callback<CB_IM>((cb) =>
            {
                if (cb.Username == "inserted@gmail.com" &&
                    cb.ContactId == 1 &&
                    cb.IMTypeId == 1)
                {
                    insertValue = true;
                }
            });
            imRepo.Setup(nm => nm.Delete(It.IsAny<CB_IM>())).Callback<CB_IM>((cb) =>
            {
                if (cb.Username == "abc1@gmail.com" &&
                    cb.ContactId == 1 &&
                    cb.IMTypeId == 1 &&
                    cb.IMId == 1)
                {
                    deleteValue = true;
                }
            });
            imRepo.Setup(nm => nm.Update(It.IsAny<CB_IM>())).Callback<CB_IM>((cb) =>
            {
                if (cb.Username == "arif@gmail.com" &&
                    cb.ContactId == 1 &&
                    cb.IMTypeId == 1 &&
                    cb.IMId == 2)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_IM>()).Returns(() => imRepo.Object);

            //act
            IContactContext contactContextTest = new ContactContext(uow.Object, uow.Object);
            contactContextTest.UpdateContact(modelContactList.First());

            //assert
            Assert.True(insertValue);
            Assert.True(updateValue);
            Assert.True(deleteValue);
        }

        [Fact]
        public void UpdateContactTestCBAddressCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var addresssList = new List<CB_Address>(){
                new CB_Address(){AddressId=1, AddressTypeId=1, ContactId=1, Address="abc1"},
                new CB_Address(){AddressId=2, AddressTypeId=1, ContactId=1, Address="abc2"},
                new CB_Address(){AddressId=3, AddressTypeId=1, ContactId=1, Address="abc3"}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_Addresses = addresssList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", Addresses = new List<Address>(){
                new Address(){AddressId=2, AddressTypeId=1, ContactId=1, AddressDescription="Mumbai"},
                new Address(){AddressId=3, AddressTypeId=1, ContactId=1, AddressDescription="Delhi"},
                new Address(){AddressId=0, AddressTypeId=1, ContactId=1, AddressDescription="inserted"},
            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_Address>> addressRepo = contactFixture.MockRepository<CB_Address>(addresssList);
            addressRepo.Setup(nm => nm.Insert(It.IsAny<CB_Address>())).Callback<CB_Address>((cb) =>
            {
                if (cb.Address == "inserted" &&
                    cb.ContactId == 1 &&
                    cb.AddressTypeId == 1)
                {
                    insertValue = true;
                }
            });
            addressRepo.Setup(nm => nm.Delete(It.IsAny<CB_Address>())).Callback<CB_Address>((cb) =>
            {
                if (cb.Address == "abc1" &&
                    cb.ContactId == 1 &&
                    cb.AddressTypeId == 1 &&
                    cb.AddressId == 1)
                {
                    deleteValue = true;
                }
            });
            addressRepo.Setup(nm => nm.Update(It.IsAny<CB_Address>())).Callback<CB_Address>((cb) =>
            {
                if (cb.Address == "Mumbai" &&
                    cb.ContactId == 1 &&
                    cb.AddressTypeId == 1 &&
                    cb.AddressId == 2)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_Address>()).Returns(() => addressRepo.Object);

            //act
            IContactContext contactContextTest = new ContactContext(uow.Object, uow.Object);
            contactContextTest.UpdateContact(modelContactList.First());

            //assert
            Assert.True(insertValue);
            Assert.True(updateValue);
            Assert.True(deleteValue);
        }

        [Fact]
        public void UpdateContactTestCBInternetCallsCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var internetcallsList = new List<CB_InternetCall>(){
                new CB_InternetCall(){InternetCallId=1, ContactId=1, InternetCallNumber="999999999"},
                new CB_InternetCall(){InternetCallId=2, ContactId=1, InternetCallNumber="888888888"},
                new CB_InternetCall(){InternetCallId=3, ContactId=1, InternetCallNumber="888888888"}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_InternetCalls = internetcallsList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", InternetCalls = new List<InternetCall>(){
                new InternetCall(){InternetCallId=2, ContactId=1, InternetCallNumber="000000000"},
                new InternetCall(){InternetCallId=0,  ContactId=1, InternetCallNumber="777777777"}
            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_InternetCall>> numberRepo = contactFixture.MockRepository<CB_InternetCall>(internetcallsList);
            numberRepo.Setup(nm => nm.Insert(It.IsAny<CB_InternetCall>())).Callback<CB_InternetCall>((cb) =>
            {
                if (cb.InternetCallNumber == "777777777" &&
                    cb.ContactId == 1)
                {
                    insertValue = true;
                }
            });
            numberRepo.Setup(nm => nm.Delete(It.IsAny<CB_InternetCall>())).Callback<CB_InternetCall>((cb) =>
            {
                if (cb.InternetCallNumber == "999999999" &&
                    cb.ContactId == 1 &&
                    cb.InternetCallId == 1)
                {
                    deleteValue = true;
                }
            });
            numberRepo.Setup(nm => nm.Update(It.IsAny<CB_InternetCall>())).Callback<CB_InternetCall>((cb) =>
            {
                if (cb.InternetCallNumber == "000000000" &&
                    cb.ContactId == 1 &&
                    cb.InternetCallId == 2)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_InternetCall>()).Returns(() => numberRepo.Object);

            //act
            IContactContext contactContextTest = new ContactContext(uow.Object, uow.Object);
            contactContextTest.UpdateContact(modelContactList.First());

            //assert
            Assert.True(insertValue);
            Assert.True(updateValue);
            Assert.True(deleteValue);
        }

        [Fact]
        public void UpdateContactTestCBWebsitesCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var internetcallsList = new List<CB_Website>(){
                new CB_Website(){WebsiteId=1, ContactId=1, Website="http://a1.com"},
                new CB_Website(){WebsiteId=2, ContactId=1, Website="http://a2.com"},
                new CB_Website(){WebsiteId=3, ContactId=1, Website="http://a3.com"}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_Websites = internetcallsList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", Websites = new List<Website>(){
                new Website(){WebsiteId=2, ContactId=1, WebsiteUrl="http://www.yahoo.com"},
                new Website(){WebsiteId=0,  ContactId=1, WebsiteUrl="http://inserted.com"}
            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_Website>> websiteRepo = contactFixture.MockRepository<CB_Website>(internetcallsList);
            websiteRepo.Setup(nm => nm.Insert(It.IsAny<CB_Website>())).Callback<CB_Website>((cb) =>
            {
                if (cb.Website == "http://inserted.com" &&
                    cb.ContactId == 1)
                {
                    insertValue = true;
                }
            });
            websiteRepo.Setup(nm => nm.Delete(It.IsAny<CB_Website>())).Callback<CB_Website>((cb) =>
            {
                if (cb.Website == "http://a1.com" &&
                    cb.ContactId == 1 &&
                    cb.WebsiteId == 1)
                {
                    deleteValue = true;
                }
            });
            websiteRepo.Setup(nm => nm.Update(It.IsAny<CB_Website>())).Callback<CB_Website>((cb) =>
            {
                if (cb.Website == "http://www.yahoo.com" &&
                    cb.ContactId == 1 &&
                    cb.WebsiteId == 2)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_Website>()).Returns(() => websiteRepo.Object);

            //act
            IContactContext contactContextTest = new ContactContext(uow.Object, uow.Object);
            contactContextTest.UpdateContact(modelContactList.First());

            //assert
            Assert.True(insertValue);
            Assert.True(updateValue);
            Assert.True(deleteValue);
        }

        [Fact]
        public void UpdateContactTestCBRelationshipsCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var relationshipsList = new List<CB_Relationship>(){
                new CB_Relationship(){RelationshipId=1, RelationshipTypeId=1, ContactId=1},
                new CB_Relationship(){RelationshipId=2, RelationshipTypeId=1, ContactId=1},
                new CB_Relationship(){RelationshipId=3, RelationshipTypeId=1, ContactId=1}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_Relationships = relationshipsList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", Relationships = new List<Relationship>(){
                new Relationship(){RelationshipId=2, RelationshipTypeId=2, ContactId=1},
                new Relationship(){RelationshipId=0, RelationshipTypeId=2, ContactId=1}
            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_Relationship>> relationshipRepo = contactFixture.MockRepository<CB_Relationship>(relationshipsList);
            relationshipRepo.Setup(nm => nm.Insert(It.IsAny<CB_Relationship>())).Callback<CB_Relationship>((cb) =>
            {
                if (cb.ContactId == 1 &&
                    cb.RelationshipTypeId == 2)
                {
                    insertValue = true;
                }
            });
            relationshipRepo.Setup(nm => nm.Delete(It.IsAny<CB_Relationship>())).Callback<CB_Relationship>((cb) =>
            {
                if (
                    cb.ContactId == 1 &&
                    cb.RelationshipTypeId == 1 &&
                    cb.RelationshipId == 1)
                {
                    deleteValue = true;
                }
            });
            relationshipRepo.Setup(nm => nm.Update(It.IsAny<CB_Relationship>())).Callback<CB_Relationship>((cb) =>
            {
                if (
                    cb.ContactId == 1 &&
                    cb.RelationshipTypeId == 2 &&
                    cb.RelationshipId == 2)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_Relationship>()).Returns(() => relationshipRepo.Object);

            //act
            IContactContext contactContextTest = new ContactContext(uow.Object, uow.Object);
            contactContextTest.UpdateContact(modelContactList.First());

            //assert
            Assert.True(insertValue);
            Assert.True(updateValue);
            Assert.True(deleteValue);
        }

        [Fact]
        public void UpdateContactTestCBSpecialDatesCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var specialDatesList = new List<CB_SpecialDate>(){
                new CB_SpecialDate(){SpecialDateId=1, SpecialDateTpId=1, ContactId=1, Dates= DateTime.Parse("10/01/1985")},
                new CB_SpecialDate(){SpecialDateId=2, SpecialDateTpId=1, ContactId=1, Dates=DateTime.Parse("10/01/1986")},
                new CB_SpecialDate(){SpecialDateId=3, SpecialDateTpId=1, ContactId=1, Dates=DateTime.Parse("10/01/1987")}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_SpecialDates = specialDatesList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", SpecialDates = new List<SpecialDate>(){
                new SpecialDate(){SpecialDateId=2, SpecialDateTpId=1, ContactId=1, Dates=DateTime.Parse("10/01/1988")},
                new SpecialDate(){SpecialDateId=0, SpecialDateTpId=2, ContactId=1, Dates=DateTime.Parse("10/01/2015")}
            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_SpecialDate>> specialDateRepo = contactFixture.MockRepository<CB_SpecialDate>(specialDatesList);
            specialDateRepo.Setup(nm => nm.Insert(It.IsAny<CB_SpecialDate>())).Callback<CB_SpecialDate>((cb) =>
            {
                if (cb.Dates == DateTime.Parse("10/01/2015") &&
                    cb.ContactId == 1 &&
                    cb.SpecialDateTpId == 2)
                {
                    insertValue = true;
                }
            });
            specialDateRepo.Setup(nm => nm.Delete(It.IsAny<CB_SpecialDate>())).Callback<CB_SpecialDate>((cb) =>
            {
                if (cb.Dates == DateTime.Parse("10/01/1985") &&
                    cb.ContactId == 1 &&
                    cb.SpecialDateTpId == 1 &&
                    cb.SpecialDateId == 1)
                {
                    deleteValue = true;
                }
            });
            specialDateRepo.Setup(nm => nm.Update(It.IsAny<CB_SpecialDate>())).Callback<CB_SpecialDate>((cb) =>
            {
                if (cb.Dates == DateTime.Parse("10/01/1988") &&
                    cb.ContactId == 1 &&
                    cb.SpecialDateTpId == 1 &&
                    cb.SpecialDateId == 2)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_SpecialDate>()).Returns(() => specialDateRepo.Object);

            //act
            IContactContext contactContextTest = new ContactContext(uow.Object, uow.Object);
            contactContextTest.UpdateContact(modelContactList.First());

            //assert
            Assert.True(insertValue);
            Assert.True(updateValue);
            Assert.True(deleteValue);
        }


        [Fact]
        public void UpdateContactTestCBContactByGroupsCRUD()
        {
            //arrange
            bool insertValue = false;
            bool deleteValue = false;
            bool updateValue = false;
            var contactByGroupsList = new List<CB_ContactByGroup>(){
                new CB_ContactByGroup(){GroupRelationId=1,  ContactId=1, GroupId=1},
                new CB_ContactByGroup(){GroupRelationId=2,  ContactId=1, GroupId=2},
                new CB_ContactByGroup(){GroupRelationId=3,  ContactId=2, GroupId=1}
            };
            var updateContactList = new List<CB_Contact>() {
            new CB_Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", CB_ContactByGroups = contactByGroupsList}
            };

            var modelContactList = new List<Contact>() {
                new Contact(){ ContactId = 1, BookId = 1, Firstname = "Arif", Lastname="Khan", ContactByGroups = new List<ContactByGroup>(){
                new ContactByGroup(){GroupRelationId=2,  ContactId=1, GroupId=4},
                new ContactByGroup(){GroupRelationId=0,  ContactId=1, GroupId=3}
            }}
            };

            var cbContactList = new List<CB_Contact>() { };
            Mock<IContactBookRepositoryUow> uow = new Mock<IContactBookRepositoryUow>();
            Mock<IContactBookDbRepository<CB_Contact>> contactRepo = contactFixture.MockRepository<CB_Contact>(updateContactList);
            contactRepo.Setup(c => c.GetById(It.IsAny<object>())).Returns<object>(id => updateContactList.First());
            Mock<IContactBookDbRepository<CB_ContactByGroup>> contactByGroupRepo = contactFixture.MockRepository<CB_ContactByGroup>(contactByGroupsList);
            contactByGroupRepo.Setup(nm => nm.Insert(It.IsAny<CB_ContactByGroup>())).Callback<CB_ContactByGroup>((cb) =>
            {
                if (cb.GroupId == 3 &&
                    cb.ContactId == 1 )
                {
                    insertValue = true;
                }
            });
            contactByGroupRepo.Setup(nm => nm.Delete(It.IsAny<CB_ContactByGroup>())).Callback<CB_ContactByGroup>((cb) =>
            {
                if (cb.GroupRelationId == 1 && 
                    cb.GroupId == 1 &&
                    cb.ContactId == 1)
                {
                    deleteValue = true;
                }
            });
            contactByGroupRepo.Setup(nm => nm.Update(It.IsAny<CB_ContactByGroup>())).Callback<CB_ContactByGroup>((cb) =>
            {
                if (cb.GroupRelationId == 2 &&
                    cb.GroupId == 4 &&
                    cb.ContactId == 1)
                {
                    updateValue = true;
                }
            });

            uow.Setup(e => e.GetEntityByType<CB_Contact>()).Returns(() => contactRepo.Object);
            uow.Setup(e => e.GetEntityByType<CB_ContactByGroup>()).Returns(() => contactByGroupRepo.Object);

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
            Contact contact = DependencyFactory.Resolve<IContactContext>().GetContact(2);

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
                IContactContext conUpdate = DependencyFactory.Resolve<IContactContext>();
                conUpdate.UpdateContact(contact);
            }
        }
    }
}