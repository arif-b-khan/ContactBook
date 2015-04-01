using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class AddressType
    {
        public int AddressTypeId { get; set; }
        
        [Required(ErrorMessage="Address type cannot be empty.")]
        [Display(Name="AddressType")]
        public string AddressTypeName { get; set; }
        

        public Nullable<long> BookId { get; set; }
    }
}
