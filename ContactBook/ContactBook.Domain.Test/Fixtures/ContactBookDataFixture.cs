using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using Moq;

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

            repository.Setup(rp => rp.Get()).Returns(() =>
            {
                return plist;
            });

            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>())).Returns<Expression<Func<T, bool>>>(e =>
                {
                    if (plist != null)
                    {
                        return plist.Where(e.Compile());
                    }
                    else
                    {
                        return plist;
                    }
                });

            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>())).Returns<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>((wh, od) =>
            {
                if (plist == null)
                {
                    return null;
                }
                return od.Compile().Invoke(plist.Where(wh.Compile()).AsQueryable()).AsEnumerable();
            });

            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>(), It.IsAny<string>())).Returns<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>, string>((wh, od, str) =>
            {
                if (plist == null)
                {
                    return null;
                }
                return od.Compile().Invoke(plist.Where(wh.Compile()).AsQueryable()).AsEnumerable();
            });

            return repository.Object;
        }


        public Mock<IContactBookDbRepository<T>> MockRepository<T>(List<T> plist) where T : class
        {
            var repository = new Mock<IContactBookDbRepository<T>>();

            repository.Setup(rp => rp.Get()).Returns(() =>
            {
                return plist;
            });

            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>())).Returns<Expression<Func<T, bool>>>(e =>
                {
                    if (plist != null)
                    {
                        return plist.Where(e.Compile());
                    }
                    else
                    {
                        return plist;
                    }
                });

            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>())).Returns<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>((wh, od) =>
            {
                if (plist == null)
                {
                    return null;
                }
                return od.Compile().Invoke(plist.Where(wh.Compile()).AsQueryable()).AsEnumerable();
            });

            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>(), It.IsAny<string>())).Returns<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>, string>((wh, od, str) =>
            {
                if (plist == null)
                {
                    return null;
                }
                return od.Compile().Invoke(plist.Where(wh.Compile()).AsQueryable()).AsEnumerable();
            });

            return repository;
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

        public virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                uow.Dispose();
                container.Dispose();
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                Dispose(true);
                disposed = true;
            }
        }
    }
}