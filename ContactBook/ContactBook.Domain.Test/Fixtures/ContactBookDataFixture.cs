using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ContactBook.Domain.Test.Fixtures
{
    public class ContactBookDataFixture : IDisposable
    {
        private bool disposed = false;
        private ContactBookRepositoryUow uow;
        private ContactBookEdmContainer container;

        public ContactBookDataFixture()
        {
            container = DependencyFactory.Resolve<ContactBookEdmContainer>();
            uow = new ContactBookRepositoryUow(container);
        }

        public IContactBookDbRepository<T> Repository<T>(List<T> plist) where T : class
        {
            Mock<IContactBookDbRepository<T>> repository = new Mock<IContactBookDbRepository<T>>();
            repository.Setup(rp => rp.Get()).Returns(plist);
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>())).Returns(plist);
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>())).Returns(plist);
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>(), "")).Returns(plist);
            return repository.Object;
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

        public void DeleteContactBookModel(ContactBookInfo modelContact)
        {
            IContactBookDbRepository<CB_ContactBook> cb = uow.GetEntityByType<CB_ContactBook>();
            var contactBook = cb.Get(c => c.Username == modelContact.Username);
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