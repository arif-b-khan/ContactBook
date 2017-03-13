using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class ContactByGroup
        : IEquatable<ContactByGroup>, INewEntity<ContactByGroup>, IEntityCloneable<ContactByGroup>
    {
        public static IEqualityComparer<ContactByGroup> Comparer
        {
            get
            {
                return new CBContactByGroupComparer();
            }
        }

        public class CBContactByGroupComparer : IEqualityComparer<ContactByGroup>
        {
            public CBContactByGroupComparer()
            {

            }
            public bool Equals(ContactByGroup x, ContactByGroup y)
            {
                if (x.GroupRelationId == y.GroupRelationId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(ContactByGroup obj)
            {
                return obj.GroupRelationId.GetHashCode();
            }
        }

        public bool Equals(ContactByGroup other)
        {
            if (other.GroupRelationId.Equals(this.GroupRelationId)
                && other.GroupId.Equals(this.GroupId)
                && other.ContactId.Equals(this.ContactId))
            {
                return true;
            }
            return false;
        }

        public bool Equals(int id)
        {
            if (this.GroupRelationId.Equals(id))
            {
                return true;
            }
            return false;
        }

        public ContactByGroup Clone()
        {
            ContactByGroup cloneContactByGroup = new ContactByGroup();
            cloneContactByGroup.ContactId = this.ContactId;
            cloneContactByGroup.GroupRelationId = this.GroupRelationId;
            cloneContactByGroup.GroupId = this.GroupId;
            return cloneContactByGroup;
        }
    
    }
}
