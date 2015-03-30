using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlInternetCall
    {
        public int InternetCallId { get; set; }
        public string InternetCallNumber { get; set; }
        public long ContactId { get; set; }
    }
}
