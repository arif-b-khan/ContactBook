using ContactBook.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class NumberType : IEquatable<NumberType>
    {
        public int NumberTypeId { get; set; }

        [Required]
        [TypeExists("BookId")]
        public string NumberTypeName { get; set; }

        [ValidateBookId]
        public Nullable<long> BookId { get; set; }

        public bool Equals(NumberType other)
        {
            if (other != null)
            {
                return this.NumberTypeId == other.NumberTypeId && this.NumberTypeName == other.NumberTypeName && this.BookId == other.BookId;
            }
            return false;
        }
    }
}