using ContactBook.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class RelationshipType : IEquatable<RelationshipType>
    {
        public int RelationshipTypeId { get; set; }

        [Required]
        [TypeExists("BookId")]
        public string RelationshipTypeName { get; set; }

        [ValidateBookId]
        public Nullable<long> BookId { get; set; }

        public bool Equals(RelationshipType other)
        {
            if (other != null)
            {
                return this.RelationshipTypeId == other.RelationshipTypeId && this.RelationshipTypeName == other.RelationshipTypeName && this.BookId == other.BookId;
            }
            return false;
        }
    }
}