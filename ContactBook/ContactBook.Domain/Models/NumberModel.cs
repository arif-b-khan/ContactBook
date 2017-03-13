using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class NumberModel
    {
        public int NumberId { get; set; }

        public long ContactId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; }

        public Nullable<int> NumberTypeId { get; set; }
    }
}