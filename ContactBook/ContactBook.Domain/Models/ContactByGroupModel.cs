using System;
using ContactBook.Domain.Validations;

namespace ContactBook.Domain.Models
{
    public class ContactByGroupModel
    {
        public int GroupRelationId { get; set; }
        
        [ValidateContactId]
        public long ContactId { get; set; }

        public Nullable<int> GroupId { get; set; }

    }
}