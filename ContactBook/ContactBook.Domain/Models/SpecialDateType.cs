using ContactBook.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class SpecialDateType : IEquatable<SpecialDateType>
    {
        public int SpecialDateTpId { get; set; }

        [Required]
        [TypeExists("BookId")]
        public string DateTypeName { get; set; }

        [ValidateBookId]
        public Nullable<long> BookId { get; set; }

        public bool Equals(SpecialDateType other)
        {
            if (other != null)
            {
                return this.SpecialDateTpId == other.SpecialDateTpId && this.DateTypeName == other.DateTypeName && this.BookId == other.BookId;
            }
            return false;
       
        }
    }
}
