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
    public class ContactBookContext: ContactBaseContext, IContactBookContext
    {
        IUserInfoContext userContext;

        public ContactBookContext()
            : this("")
        {
        }

        public ContactBookContext(string connectionName)
            : base(connectionName)
        {

        }
        
        public IUserInfoContext UserInfoContext { get; set; }

        public void AddContactBook(MdlContactBook mCb)
        {
            using (var uow = new ContactBookRepositoryUow(GetContainer))
            {
                var cb = uow.GetEntityByType<CB_ContactBook>();
                Mapper.CreateMap<MdlContactBook, CB_ContactBook>();
                CB_ContactBook contactBook = Mapper.Map<CB_ContactBook>(mCb);
                cb.Insert(contactBook);
                uow.Save();
            }
        }

        public MdlContactBook GetContactBook(string userId)
        {
            using (var uow = new ContactBookRepositoryUow(GetContainer))
            {
                CB_ContactBook cb = uow.GetEntityByType<CB_ContactBook>().Get(c => c.AspNetUserId == userId).FirstOrDefault();
                Mapper.CreateMap<CB_ContactBook, MdlContactBook>();
                return Mapper.Map<MdlContactBook>(cb);
            }
        }

        public void CreateContactBook(string userName)
        {
            if(UserInfoContext != null)
            {
                AspNetUser userInfo = UserInfoContext.GetUserInfo(userName);
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
