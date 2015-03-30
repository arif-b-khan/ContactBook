using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlNumberType
    {
        public int NumberTypeId { get; set; }
        public string NumberType { get; set; }
        public Nullable<long> BookId { get; set; }
    }
}
