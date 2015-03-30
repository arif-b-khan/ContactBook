using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlContactByGroup
    {
        public int GroupRelationId { get; set; }
        public long ContactId { get; set; }
        public Nullable<int> GroupId { get; set; }
    }
}
