using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Contact;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.Domain.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactBook.Domain.Test.Contexts.Contact
{
    public class ContactContextTest : IClassFixture<ContactBookDataFixture>
    {
        ContactBookDataFixture contactFixture;

        public ContactContextTest(ContactBookDataFixture fixture)
        {
            this.contactFixture = fixture;
        }

        [Fact]
        public void GetContactTestHasRecord()
        {
            MdlContact mcontact;
            using (IContactBookRepositoryUow uow = DependencyFactory.Resolve<IContactBookRepositoryUow>())
            {
                IContactContext contact = new ContactContext(uow);
                mcontact = contact.GetContact(1);
            }

            Assert.NotNull(mcontact);
        }
    }
}
