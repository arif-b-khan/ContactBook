using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlRelationship
    {
        public int RelationshipId { get; set; }
        public long ContactId { get; set; }
        public Nullable<int> RelationshipTypeId { get; set; }
    }
}
