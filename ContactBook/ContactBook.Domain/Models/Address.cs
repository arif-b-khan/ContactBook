using System;

namespace ContactBook.Domain.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public long ContactId { get; set; }

        public string AddressDescription { get; set; }

        public Nullable<int> AddressTypeId { get; set; }

    }
}