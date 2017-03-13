using System.Collections.Generic;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Contexts.Contacts
{
    public interface IContactContext
    {
        List<ContactModel> GetContacts(long bookId);
        ContactModel GetContact(long contactId);
        ContactModel GetContact(long bookId, long contactId);
        void InsertContact(ContactModel contact);
        void DeleteContact(ContactModel contact);
        void UpdateContact(ContactModel contact);
    }
}