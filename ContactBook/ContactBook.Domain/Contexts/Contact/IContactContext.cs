using System;
using ContactBook.Domain.Models;
namespace ContactBook.Domain.Contexts.Contact
{
    public interface IContactContext
    {
        MdlContact GetContact(long contactId);
    }
}
