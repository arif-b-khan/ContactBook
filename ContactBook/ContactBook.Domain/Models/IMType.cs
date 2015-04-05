using ContactBook.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class IMType
    {
        public int IMTypeId { get; set; }

        [Required]
        [TypeExists("BookId")]
        public string IMTypeName { get; set; }

        [ValidateBookId]
        public Nullable<long> BookId { get; set; }
    }
}