using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlAddressType
    {
        public int AddressTypeId { get; set; }
        
        [Required]
        [Display(Name="AddressType")]
        public string Address_TypeName { get; set; }
        public Nullable<long> BookId { get; set; }
    }
}
