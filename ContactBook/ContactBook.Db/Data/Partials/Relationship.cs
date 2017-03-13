using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class Relationship : IEquatable<Relationship>, INewEntity<Relationship>, IEntityCloneable<Relationship>
    {
        public static IEqualityComparer<Relationship> Comparer
        {
            get
            {
                return new CBRelationshipComparer();
            }
        }

        public class CBRelationshipComparer : IEqualityComparer<Relationship>
        {
            public CBRelationshipComparer()
            {

            }
            public bool Equals(Relationship x, Relationship y)
            {
                if (x.RelationshipId == y.RelationshipId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Relationship obj)
            {
                return obj.RelationshipId.GetHashCode();
            }
        }

        public bool Equals(Relationship other)
        {
            if (other.RelationshipId.Equals(this.RelationshipId)
                && other.RelationshipTypeId.Equals(this.RelationshipTypeId)
                && other.ContactId.Equals(this.ContactId))
            {
                return true;
            }
            return false;
        }

        public bool Equals(int id)
        {
            if (this.RelationshipId.Equals(id))
            {
                return true;
            }
            return false;
        }

        public Relationship Clone()
        {
            Relationship cloneRelationship = new Relationship();
            cloneRelationship.ContactId = this.ContactId;
            cloneRelationship.RelationshipId = this.RelationshipId;
            cloneRelationship.RelationshipTypeId = this.RelationshipTypeId;
            return cloneRelationship;
        }

    }
}
