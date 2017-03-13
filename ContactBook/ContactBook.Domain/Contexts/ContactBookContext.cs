using AutoMapper;
using data = ContactBook.Db.Data;
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
        private IContactBookDbRepository<data.ContactBook> conBookRepo;

        public ContactBookContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            conBookRepo = unitOfWork.GetEntityByType<data.ContactBook>();
        }

        public void AddContactBook(ContactBookInfoModel mCb)
        {
            Mapper.CreateMap<ContactBookInfoModel, data.ContactBook>();
            data.ContactBook contactBook = Mapper.Map<data.ContactBook>(mCb);
            conBookRepo.Insert(contactBook);
            unitOfWork.Save();
        }

        public ContactBookInfoModel GetContactBook(string userName)
        {
            ContactBookInfoModel retBook = null;

            try
            {
                data.ContactBook cb = conBookRepo.Get(c => c.Username == userName).FirstOrDefault();
                Mapper.CreateMap<data.ContactBook, ContactBookInfoModel>();
                retBook = Mapper.Map<ContactBookInfoModel>(cb);
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
            this.AddContactBook(new ContactBookInfoModel()
            {
                Username = userName,
                BookName = Id + "-" + userName,
                Enabled = true
            });
            unitOfWork.Save();
        }
    }
}