using System;

namespace ContactBook.Domain.Models
{
    public class ContactByGroup
    {
        public int GroupRelationId { get; set; }

        public long ContactId { get; set; }

        public Nullable<int> GroupId { get; set; }

    }
}