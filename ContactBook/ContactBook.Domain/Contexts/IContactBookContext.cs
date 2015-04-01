using System;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Contexts
{
    public interface IContactBookContext
    {
        void AddContactBook(ContactBookInfo mCb);
        ContactBookInfo GetContactBook(string userName);
        void CreateContactBook(string username, string Id);
    }
}
