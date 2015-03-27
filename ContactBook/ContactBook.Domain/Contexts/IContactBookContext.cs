using System;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Contexts
{
    public interface IContactBookContext
    {
        void AddContactBook(MdlContactBook mCb);
        MdlContactBook GetContactBook(string userId);
        void CreateContactBook(string username);
    }
}
