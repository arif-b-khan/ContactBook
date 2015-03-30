using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlSpecialDates
    {
        public int SpecialDateId { get; set; }
        public long ContactId { get; set; }
        public System.DateTime Dates { get; set; }
        public int SpecialDateTpId { get; set; }
    
    }
}
