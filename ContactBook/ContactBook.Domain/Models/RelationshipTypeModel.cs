using ContactBook.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class RelationshipTypeModel : IEquatable<RelationshipTypeModel>
    {
        public int RelationshipTypeId { get; set; }

        [Required]
        [TypeExists("BookId")]
        public string RelationshipTypeName { get; set; }

        [ValidateBookId]
        public Nullable<long> BookId { get; set; }

        public bool Equals(RelationshipTypeModel other)
        {
            if (other != null)
            {
                return this.RelationshipTypeId == other.RelationshipTypeId && this.RelationshipTypeName == other.RelationshipTypeName && this.BookId == other.BookId;
            }
            return false;
        }
    }
}