using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlAddress
    {
        public int AddressId { get; set; }
        public long ContactId { get; set; }
        public string Address { get; set; }
        public Nullable<int> AddressTypeId { get; set; }
    }
}
