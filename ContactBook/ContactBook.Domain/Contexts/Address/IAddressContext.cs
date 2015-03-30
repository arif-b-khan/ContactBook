using System;
using System.Collections.Generic;
using ContactBook.Domain.Models;
namespace ContactBook.Domain.Contexts.Address
{
    interface IAddressContext
    {
        void DeleteAddresses(List<MdlAddress> addresses);
        List<MdlAddress> GetAddressContactId(long contactId);
        void InsertAddresses(List<MdlAddress> addresses);
        void UpdateAddresses(List<MdlAddress> addresses);
    }
}
