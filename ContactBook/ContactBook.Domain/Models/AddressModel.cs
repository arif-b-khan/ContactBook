using System;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }

        public long ContactId { get; set; }

        [Required]
        [StringLength(3000)]
        public string AddressDescription { get; set; }

        public Nullable<int> AddressTypeId { get; set; }

    }
}