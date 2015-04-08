using System.Collections.Generic;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Contexts.Contacts
{
    public interface IContactContext
    {
        List<Contact> GetContacts(long bookId);
        Contact GetContact(long contactId);
        Contact GetContact(long bookId, long contactId);
        void InsertContact(Contact contact);
        void DeleteContact(Contact contact);
        void UpdateContact(Contact contact);
    }
}