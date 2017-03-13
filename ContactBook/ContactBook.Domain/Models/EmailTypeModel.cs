using ContactBook.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class EmailTypeModel : IEquatable<EmailTypeModel>
    {
        public int EmailTypeId { get; set; }

        [Required]
        [TypeExists("BookId")]
        public string EmailTypeName { get; set; }

        [ValidateBookId]
        public Nullable<long> BookId { get; set; }

        public bool Equals(EmailTypeModel other)
        {
            if (other != null)
            {
                return this.EmailTypeId == other.EmailTypeId && this.EmailTypeName == other.EmailTypeName && this.BookId == other.BookId;
            }
            return false;
        }
    }
}