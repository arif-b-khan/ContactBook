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
    public class ContactBookContext
    {
        public void AddContactBook(MdlContactBook mCb)
        {
            using (var uow = new ContactBookRepositoryUow(new ContactBookEdmContainer()))
            {
                var cb = uow.GetEntityByType<CB_ContactBook>();
                cb.Insert(Mapper.Map<MdlContactBook, CB_ContactBook>(mCb));
                uow.Save();
            }
        }

        public MdlContactBook GetContactBook(string userId)
        {
            using (var uow = new ContactBookRepositoryUow(new ContactBookEdmContainer()))
            {
                CB_ContactBook cb = uow.GetEntityByType<CB_ContactBook>().Get(c => c.AspNetUserId == userId).FirstOrDefault();
                return Mapper.Map<CB_ContactBook, MdlContactBook>(cb);
            }
        }
    }
}
