using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlNumber
    {
        public int NumberId { get; set; }
        public long ContactId { get; set; }
        public string Number { get; set; }
        public Nullable<int> NumberTypeId { get; set; }
    }
}
