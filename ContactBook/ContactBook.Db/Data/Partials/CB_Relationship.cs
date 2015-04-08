using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class CB_Relationship : IEquatable<CB_Relationship>, INewEntity<CB_Relationship>, IEntityCloneable<CB_Relationship>
    {
        public static IEqualityComparer<CB_Relationship> Comparer
        {
            get
            {
                return new CBRelationshipComparer();
            }
        }

        public class CBRelationshipComparer : IEqualityComparer<CB_Relationship>
        {
            public CBRelationshipComparer()
            {

            }
            public bool Equals(CB_Relationship x, CB_Relationship y)
            {
                if (x.RelationshipId == y.RelationshipId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(CB_Relationship obj)
            {
                return obj.RelationshipId.GetHashCode();
            }
        }

        public bool Equals(CB_Relationship other)
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

        public CB_Relationship Clone()
        {
            CB_Relationship cloneRelationship = new CB_Relationship();
            cloneRelationship.ContactId = this.ContactId;
            cloneRelationship.RelationshipId = this.RelationshipId;
            cloneRelationship.RelationshipTypeId = this.RelationshipTypeId;
            return cloneRelationship;
        }

    }
}
