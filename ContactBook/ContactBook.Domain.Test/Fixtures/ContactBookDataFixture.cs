using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using Moq;

namespace ContactBook.Domain.Test.Fixtures
{
    public class ContactBookDataFixture : IDisposable
    {
        List<AspNetUser> list;
        bool disposed = false;
        ContactBookRepositoryUow uow;
        ContactBookEdmContainer container;
        Mock<ContactBookDbRepository<AspNetUser>> contactRepo;

        public ContactBookDataFixture()
        {
            list = new List<AspNetUser>(){
            new AspNetUser(){Id = "1", UserName="user1"}
            };
            container = DependencyFactory.Resolve<ContactBookEdmContainer>();
            uow = new ContactBookRepositoryUow(container);

            contactRepo = new Mock<ContactBookDbRepository<AspNetUser>>(Container);
            contactRepo.Setup(rp => rp.Get()).Returns(list);
            contactRepo.Setup(rp => rp.Get(It.IsAny<Expression<Func<AspNetUser, bool>>>())).Returns(list);
            contactRepo.Setup(rp => rp.Get(It.IsAny<Expression<Func<AspNetUser, bool>>>(), It.IsAny<Expression<Func<IQueryable<AspNetUser>, IOrderedQueryable<AspNetUser>>>>())).Returns(list);
            contactRepo.Setup(rp => rp.Get(It.IsAny<Expression<Func<AspNetUser, bool>>>(), It.IsAny<Expression<Func<IQueryable<AspNetUser>, IOrderedQueryable<AspNetUser>>>>(), "")).Returns(list);
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

        public ContactBookDbRepository<AspNetUser> ContactBookDbRepository
        {
            get
            {
                return contactRepo.Object;
            }
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
            IContactBookDbRepository<CB_ContactBook> cb = uow.GetEntityByType<CB_ContactBook>();
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
