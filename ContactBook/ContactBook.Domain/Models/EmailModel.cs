using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class EmailModel
    {
        public int EmailId { get; set; }

        public long ContactId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        public Nullable<int> EmailTypeId { get; set; }

    }
}