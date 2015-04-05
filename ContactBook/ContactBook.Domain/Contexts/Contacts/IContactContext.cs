using ContactBook.Domain.Models;

namespace ContactBook.Domain.Contexts.Contacts
{
    public interface IContactContext
    {
        Contact GetContact(long contactId);
        void InsertContact(Contact contact);
        void DeleteContact(Contact contact);
        void UpdateContact(Contact contact);
    }
}