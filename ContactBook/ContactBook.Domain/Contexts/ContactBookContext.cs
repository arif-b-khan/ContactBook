using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using AutoMapper;

namespace ContactBook.Domain.Contexts
{
    public class ContactBookContext : IContactBookContext
    {
        IUserInfoContext userContext;
        IContactBookRepositoryUow unitOfWork;
        IContactBookDbRepository<CB_ContactBook> conBookRepo;

        public ContactBookContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            conBookRepo = unitOfWork.GetEntityByType<CB_ContactBook>();
        }

        public ContactBookContext(IContactBookRepositoryUow unitOfWork, IUserInfoContext userContext) : this(unitOfWork)
        {
            this.userContext = userContext;
        }
        
        public void AddContactBook(MdlContactBook mCb)
        {
            Mapper.CreateMap<MdlContactBook, CB_ContactBook>();
            CB_ContactBook contactBook = Mapper.Map<CB_ContactBook>(mCb);
            conBookRepo.Insert(contactBook);
            unitOfWork.Save();
        }

        public MdlContactBook GetContactBook(string userId)
        {
            CB_ContactBook cb = conBookRepo.Get(c => c.AspNetUserId == userId).FirstOrDefault();
            Mapper.CreateMap<CB_ContactBook, MdlContactBook>();
            return Mapper.Map<MdlContactBook>(cb);
        }

        public void CreateContactBook(string userName)
        {
            if (userContext != null)
            {
                AspNetUser userInfo = userContext.GetUserInfo(userName);
                this.AddContactBook(new MdlContactBook()
                {
                    AspNetUserId = userInfo.Id,
                    BookName = userInfo.Id + "-" + userInfo.UserName,
                    Enabled = true
                });
            }
        }

    }
}
