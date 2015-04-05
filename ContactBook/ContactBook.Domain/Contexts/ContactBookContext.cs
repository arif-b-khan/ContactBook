using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using System;
using System.Linq;

namespace ContactBook.Domain.Contexts
{
    public class ContactBookContext : IContactBookContext
    {
        private IContactBookRepositoryUow unitOfWork;
        private IContactBookDbRepository<CB_ContactBook> conBookRepo;

        public ContactBookContext()
            : this(DependencyFactory.Resolve<IContactBookRepositoryUow>())
        {
        }

        public ContactBookContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            conBookRepo = unitOfWork.GetEntityByType<CB_ContactBook>();
        }

        public void AddContactBook(ContactBookInfo mCb)
        {
            Mapper.CreateMap<ContactBookInfo, CB_ContactBook>();
            CB_ContactBook contactBook = Mapper.Map<CB_ContactBook>(mCb);
            conBookRepo.Insert(contactBook);
            unitOfWork.Save();
        }

        public ContactBookInfo GetContactBook(string userName)
        {
            ContactBookInfo retBook = null;

            try
            {
                CB_ContactBook cb = conBookRepo.Get(c => c.Username == userName).FirstOrDefault();
                Mapper.CreateMap<CB_ContactBook, ContactBookInfo>();
                retBook = Mapper.Map<ContactBookInfo>(cb);
            }
            catch (ArgumentNullException ex)
            {
                //todo: write code to log this exception
                //throw;
            }

            return retBook;
        }

        public void CreateContactBook(string userName, string Id)
        {
            this.AddContactBook(new ContactBookInfo()
            {
                Username = userName,
                BookName = Id + "-" + userName,
                Enabled = true
            });
            unitOfWork.Save();
        }
    }
}