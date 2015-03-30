using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlEmail
    {
        public int EmailId { get; set; }
        public long ContactId { get; set; }
        public string Email { get; set; }
        public Nullable<int> EmailTypeId { get; set; }
    }
}
