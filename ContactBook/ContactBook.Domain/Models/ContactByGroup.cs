using System;
using ContactBook.Domain.Validations;

namespace ContactBook.Domain.Models
{
    public class ContactByGroup
    {
        public int GroupRelationId { get; set; }
        
        [ValidateContactId]
        public long ContactId { get; set; }

        public Nullable<int> GroupId { get; set; }

    }
}