using System;

namespace ContactBook.Domain.Models
{
    public class Relationship
    {
        public int RelationshipId { get; set; }

        public long ContactId { get; set; }

        public Nullable<int> RelationshipTypeId { get; set; }
    }
}