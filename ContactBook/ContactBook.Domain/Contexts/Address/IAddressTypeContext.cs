using System;
using System.Collections.Generic;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
namespace ContactBook.Domain.Contexts.Address
{
    public interface IAddressTypeContext : IUnitOfWork
    {
        void AddAddressTypes(List<MdlAddressType> addressType);
        List<MdlAddressType> GetAddessType(long bookId);
    }
}
