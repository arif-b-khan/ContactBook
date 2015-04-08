using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class CB_ContactByGroup : IEquatable<CB_ContactByGroup>, INewEntity<CB_ContactByGroup>, IEntityCloneable<CB_ContactByGroup>
    {
        public static IEqualityComparer<CB_ContactByGroup> Comparer
        {
            get
            {
                return new CBContactByGroupComparer();
            }
        }

        public class CBContactByGroupComparer : IEqualityComparer<CB_ContactByGroup>
        {
            public CBContactByGroupComparer()
            {

            }
            public bool Equals(CB_ContactByGroup x, CB_ContactByGroup y)
            {
                if (x.GroupRelationId == y.GroupRelationId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(CB_ContactByGroup obj)
            {
                return obj.GroupRelationId.GetHashCode();
            }
        }

        public bool Equals(CB_ContactByGroup other)
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

        public CB_ContactByGroup Clone()
        {
            CB_ContactByGroup cloneContactByGroup = new CB_ContactByGroup();
            cloneContactByGroup.ContactId = this.ContactId;
            cloneContactByGroup.GroupRelationId = this.GroupRelationId;
            cloneContactByGroup.GroupId = this.GroupId;
            return cloneContactByGroup;
        }
    
    }
}
