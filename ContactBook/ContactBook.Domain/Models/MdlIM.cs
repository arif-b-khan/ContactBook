using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlIM
    {
        public int IMId { get; set; }
        public string Username { get; set; }
        public long ContactId { get; set; }
        public int IMTypeId { get; set; }
    }
}
