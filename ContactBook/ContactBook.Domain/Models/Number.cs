using System;

namespace ContactBook.Domain.Models
{
    public class Number
    {
        public int NumberId { get; set; }

        public long ContactId { get; set; }

        public string ContactNumber { get; set; }

        public Nullable<int> NumberTypeId { get; set; }
    }
}