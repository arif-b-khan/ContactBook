using ContactBook.Domain.Models;
using System.Collections.Generic;

namespace ContactBook.Domain.Contexts.Addresses
{
    internal interface IAddressContext
    {
        void DeleteAddresses(List<Address> addresses);

        List<Address> GetAddressContactId(long contactId);

        void InsertAddresses(List<Address> addresses);

        void UpdateAddresses(List<Address> addresses);
    }
}