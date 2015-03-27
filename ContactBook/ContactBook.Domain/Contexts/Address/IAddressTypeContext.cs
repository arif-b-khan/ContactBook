using System;
using System.Collections.Generic;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
namespace ContactBook.Domain.Contexts.Address
{
    public interface IAddressTypeContext
    {
        List<MdlAddressType> GetAddessType(long bookId);
        void AddAddressTypes(List<MdlAddressType> addressType);
        void UpdateAddressTypes(List<MdlAddressType> addressTypes);
        void RemoveAddressTypes(List<MdlAddressType> addressTypes);
    }
}
