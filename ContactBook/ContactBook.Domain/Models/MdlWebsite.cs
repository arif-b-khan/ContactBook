using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlWebsite
    {
        public int WebsiteId { get; set; }
        public string Website { get; set; }
        public long ContactId { get; set; }
    }
}
