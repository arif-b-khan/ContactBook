using ContactBook.Domain.Models;

namespace ContactBook.Domain.Contexts
{
    public interface IContactBookContext
    {
        void AddContactBook(ContactBookInfoModel mCb);

        ContactBookInfoModel GetContactBook(string userName);

        void CreateContactBook(string username, string Id);
    }
}