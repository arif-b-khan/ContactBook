using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class ContactBookInfo
    {
        public long BookId { get; set; }
        public string BookName { get; set; }
        public bool Enabled { get; set; }
        public string Username { get; set; }
    }
}
