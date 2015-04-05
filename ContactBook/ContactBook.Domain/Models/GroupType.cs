using ContactBook.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class GroupType : IEquatable<GroupType>
    {
        public int GroupId { get; set; }

        [Required]
        [TypeExists("BookId")]
        public string GroupTypeName { get; set; }

        [ValidateBookId]
        public Nullable<long> BookId { get; set; }

        public bool Equals(GroupType other)
        {
            if (other != null)
            {
                return this.GroupId == other.GroupId && this.GroupTypeName == other.GroupTypeName && this.BookId == other.BookId;
            }
            return false;
        }
    }
}