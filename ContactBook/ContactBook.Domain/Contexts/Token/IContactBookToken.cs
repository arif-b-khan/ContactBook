using System;
namespace ContactBook.Domain.Contexts.Token
{
    public interface IContactBookToken
    {
        bool DeleteToken(string userId);
        bool SaveToken(string id, string token, string tokenType, Guid guid);
    }
}
