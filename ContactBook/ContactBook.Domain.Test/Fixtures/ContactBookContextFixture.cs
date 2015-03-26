using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Test.Fixtures
{
    public class ContactBookDataFixture : IDisposable
    {
        bool disposed = false;
        ContactBookRepositoryUow uow;
        ContactBookEdmContainer container;
        
        public ContactBookDataFixture()
        {
            container = new ContactBookEdmContainer("name=ContactBookEdmContainerTest");
            uow = new ContactBookRepositoryUow(container);
        }

        public ContactBookEdmContainer Container
        {
            get
            {
                return container;
            }
        }

        public ContactBookRepositoryUow UnitOfWork
        {
            get
            {
                return uow;
            }
        }

        public string Catalog
        {
            get
            {
                return "ContactBookEdmContainerTest";
            }
        }

        public void DeleteContactBookModel(MdlContactBook modelContact)
        {
            ContactBookDbRepository<CB_ContactBook> cb = uow.GetEntityByType<CB_ContactBook>();
            var contactBook = cb.Get(c => c.AspNetUserId == modelContact.AspNetUserId);
            foreach (var con in contactBook)
            {
                cb.Delete(con);
            }
            uow.Save();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                uow.Dispose();
                container.Dispose();
                disposed = true;
            }
        }
    }
}
