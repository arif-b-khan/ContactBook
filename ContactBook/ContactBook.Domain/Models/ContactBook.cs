using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlContactBook
    {
        public long BookId { get; set; }
        public string BookName { get; set; }
        public bool Enbaled { get; set; }
        public string AspNetUserId { get; set; }
    }
}
