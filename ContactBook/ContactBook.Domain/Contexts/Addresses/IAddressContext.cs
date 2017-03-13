using ContactBook.Domain.Models;
using System.Collections.Generic;

namespace ContactBook.Domain.Contexts.Addresses
{
    internal interface IAddressContext
    {
        void DeleteAddresses(List<AddressModel> addresses);

        List<AddressModel> GetAddressContactId(long contactId);

        void InsertAddresses(List<AddressModel> addresses);

        void UpdateAddresses(List<AddressModel> addresses);
    }
}