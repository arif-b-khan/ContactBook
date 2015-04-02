using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ContactBook.Domain.Validations;

namespace ContactBook.Domain.Models
{
    public class AddressType : IEquatable<AddressType>
    {
        public int AddressTypeId { get; set; }
        
        [TypeExists("BookId")]
        [Required(ErrorMessage="Address type cannot be empty.")]
        [Display(Name="AddressType")]
        public string AddressTypeName { get; set; }

        [ValidateBookId]
        public Nullable<long> BookId { get; set; }

        public bool Equals(AddressType other)
        {
            if (other != null)
            {
                return (this.AddressTypeId == other.AddressTypeId && this.AddressTypeName == other.AddressTypeName && this.BookId == other.BookId);
            }

            return false;
        }
    }
}
