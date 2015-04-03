using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Domain.Validations;

namespace ContactBook.Domain.Models
{
    public class NumberType : IEquatable<NumberType>
    {
        public int NumberTypeId { get; set; }
        
        [Required]
        [Display(Name="NumberType")]
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
